﻿using UnityEngine;
using System.Collections;

namespace HoloLearn
{

    public class QuitApplication : MonoBehaviour
    {

        public void Quit()
        {
            //If we are running in a standalone build of the game
#if WINDOWS_UWP
		//Quit the application
		Application.Quit();
#endif

            //If we are running in the editor
#if UNITY_EDITOR
            //Stop playing the scene
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
