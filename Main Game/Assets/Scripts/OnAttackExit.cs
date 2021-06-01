using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAttackExit : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bigMech burger = animator.GetComponentInParent<bigMech>();
        burger.isAttacking = false;
    }
}
