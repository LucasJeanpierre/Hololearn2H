using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.VirtualAssistant
{
    class ReactiveAssistantManager : VirtualAssistantManager
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
            IsDragging = true;
        }

        public override void ObjectDropped()
        {
            IsDragging = false;
        }


        public override void Walk()
        {
            gameObject.GetComponent<Animator>().SetTrigger("Walk");
        }


        public override void SetTriggers()
        {
            // Nothing to do
        }

        public override void CommandReceived()
        {
            if (IsDragging)
            {
                gameObject.GetComponent<Animator>().ResetTrigger("DraggingStopped");
                gameObject.GetComponent<Animator>().SetTrigger("DraggingStarted");
            }
            else
            {
                gameObject.GetComponent<Animator>().ResetTrigger("DraggingStarted");
                gameObject.GetComponent<Animator>().SetTrigger("DraggingStopped");
            }
        }
        
    }
}
