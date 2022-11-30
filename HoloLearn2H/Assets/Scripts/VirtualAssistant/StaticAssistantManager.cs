using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.VirtualAssistant
{
    class StaticAssistantManager : VirtualAssistantManager
    {

        // Use this for initialization
        public override void Start()
        {
            IsBusy = false;
        }

        // Update is called once per frame
        public override void Update()
        {
            
        }
        

        public override void ObjectDragged(GameObject draggedObject)
        {         
            // Nothing to do
        }

        public override void ObjectDropped()
        {
            // Nothing to do
        }


        public override void Walk()
        {
            // Nothing to do
        }

        public override void SetTriggers()
        {
            // Nothing to do
        }

        public override void CommandReceived()
        {
            // Nothing to do
        }

    }
}
