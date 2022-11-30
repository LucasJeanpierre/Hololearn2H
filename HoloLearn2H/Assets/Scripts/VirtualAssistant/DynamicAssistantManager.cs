using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.VirtualAssistant
{
    class DynamicAssistantManager : VirtualAssistantManager
    {
        // Use this for initialization
        public override void Start()
        {
            IsBusy = false;
            IsDragging = false;
        }

        // Update is called once per frame
        public override void Update()
        {

        }

        
        public override void ObjectDragged(GameObject draggedObject)
        {
            targetObject = draggedObject.transform;
            gameObject.GetComponent<Animator>().ResetTrigger("DraggingStopped");
            gameObject.GetComponent<Animator>().SetTrigger("DraggingStarted");
            IsDragging = true;
        }

        public override void ObjectDropped()
        {
            gameObject.GetComponent<Animator>().ResetTrigger("DraggingStarted");
            gameObject.GetComponent<Animator>().SetTrigger("DraggingStopped");
            IsDragging = false;
        }

        public override void Walk()
        {
            StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(patience);
            gameObject.GetComponent<Animator>().SetTrigger("Walk");
        }

        public override void SetTriggers()
        {
            if (IsDragging)
            {
                gameObject.GetComponent<Animator>().SetTrigger("DraggingStarted");
            }
            else
            {
                gameObject.GetComponent<Animator>().SetTrigger("DraggingStopped");
            }
        }

        public override void CommandReceived()
        {
            // Nothing to do
        }

    }
}
