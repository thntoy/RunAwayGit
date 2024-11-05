using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandIdle : StateMachineBehaviour
{
    private Action SweepAction;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SweepAction = () => animator.SetTrigger("Sweep");
        Hand.OnStartBossFight += SweepAction;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Hand.OnStartBossFight -= SweepAction;
    }
 
}
