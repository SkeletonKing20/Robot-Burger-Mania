using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAttackExit : StateMachineBehaviour
{
    Player player;

    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bigMech burger = animator.GetComponentInParent<bigMech>();
        burger.isAttacking = false;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //bigMech burger = animator.GetComponentInParent<bigMech>();
        //player = FindObjectOfType<Player>();
        //Vector3 targetPosition = player.transform.position;
        //burger.attackPosition(targetPosition);
    }
}
