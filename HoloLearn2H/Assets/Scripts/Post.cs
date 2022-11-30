using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class Post : MonoBehaviour
{
    [Header("Inserisci l'url del server senza indicare il protocollo")]
    public string url = "twbpolimi.herokuapp.com";
    [Header("Inserisci id della sessione")]
    public int idSession = 299;


    private int activityId;
    private Camera virtuCamera;
    private RenderTexture rendTexture;
    private Texture2D streaming;
    private readonly int width = 480;
    private readonly int height = 360;
    private Coroutine stream;


    private void Awake()
    {
        virtuCamera = GetComponent<Camera>();
        rendTexture = new RenderTexture(width, height, 24);
        streaming = new Texture2D(width, height, TextureFormat.RGB24, false);
        virtuCamera.aspect = width / height;
        virtuCamera.targetTexture = rendTexture;
    }

    private void Start()
    {
        stream = StartCoroutine(GetActivityID());
    }

    private void OnDestroy()
    {
        if(stream != null)
        {
            StopCoroutine(stream);
            stream = null;
        }
    }

    IEnumerator GetActivityID()
    {
        string urlTemp = "http://" + url + "/configuration/" + idSession;
        UnityWebRequest webRequest = UnityWebRequest.Get(urlTemp);
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError)
        {
            Debug.Log("Error: " + webRequest.error);
        }
        else
        {
            string res = webRequest.downloadHandler.text;
            Debug.Log("Received: " + res);
            activityId = JsonUtility.FromJson<Response>(res).id;
            StartCoroutine(SendStreaming());
        }
    }

    IEnumerator SendStreaming()
    {
        while (true)
        {
            virtuCamera.Render();
            RenderTexture.active = rendTexture;
            streaming.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            streaming.Apply();
            RenderTexture.active = null;
            string frame = "data:image/jpg; base64," + Convert.ToBase64String(streaming.EncodeToJPG(50));
            string urlTemp = "http://" + url + "/activity?id="+ idSession + "&activity=" + activityId;
            Message msg = new Message();
            msg.important = false;
            msg.data.frame = frame;
            msg.data.width = streaming.width;
            msg.data.height = streaming.height;
            string temp = JsonUtility.ToJson(msg);
            var uwr = new UnityWebRequest(urlTemp, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(temp);
            uwr.uploadHandler = new UploadHandlerRaw(jsonToSend);
            uwr.downloadHandler = new DownloadHandlerBuffer();
            uwr.SetRequestHeader("Content-Type", "application/json");

            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError)
            {
                Debug.Log("Error While Sending: " + uwr.error);
            }
            else
            {
                Debug.Log("Received: " + uwr.downloadHandler.text);
            }
            yield return new WaitForSeconds(.150f);
        }
    }

    [Serializable]
    private class Message
    {
        public Message()
        {
            data = new Data();
        }

        public bool important;
        public Data data;
    }

    [Serializable]
    private class Data
    {
        public string frame;
        public int width;
        public int height;
    }

    [Serializable]
    private class Response
    {
        public int id;
    }


}
