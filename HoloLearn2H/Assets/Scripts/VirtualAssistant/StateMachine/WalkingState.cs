using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : StateMachineBehaviour {

    private float speed;
    private Vector3 targetPosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        VirtualAssistantManager.Instance.GetComponent<Animator>().ResetTrigger("DraggingStarted");
        VirtualAssistantManager.Instance.GetComponent<Animator>().ResetTrigger("DraggingStopped");

        targetPosition = VirtualAssistantManager.Instance.targetObject.GetComponent<Rigidbody>().ClosestPointOnBounds(VirtualAssistantManager.Instance.transform.position);
        speed = VirtualAssistantManager.Instance.speed;

        VirtualAssistantManager.Instance.GetComponent<AssistantAudioManagerInterface>().PlayWalking();
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetPosition.y = VirtualAssistantManager.Instance.transform.position.y;

        Vector3 relativePos = targetPosition - VirtualAssistantManager.Instance.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        rotation.x = 0f;
        rotation.z = 0f;

        VirtualAssistantManager.Instance.transform.rotation = Quaternion.Lerp(VirtualAssistantManager.Instance.transform.rotation, rotation, Time.deltaTime * 2f);


        if (Vector3.Distance(VirtualAssistantManager.Instance.transform.position, targetPosition) > 0.05f)
        {
            Debug.DrawLine(VirtualAssistantManager.Instance.transform.position, targetPosition, Color.blue, 5f);

            VirtualAssistantManager.Instance.transform.position = Vector3.MoveTowards(VirtualAssistantManager.Instance.transform.position, targetPosition, speed);
        }
        else
        {
            animator.SetTrigger("TargetReached");
        }
    }

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
