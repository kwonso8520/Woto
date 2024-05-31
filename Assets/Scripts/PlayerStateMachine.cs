using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachineBehaviour
{
    public PlayerState behaviourState;
    public PlayerController_Platform platform;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(platform == null)
        {
            platform = animator.GetComponent<PlayerController_Platform>();
        }   
        platform.currentState = behaviourState;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
