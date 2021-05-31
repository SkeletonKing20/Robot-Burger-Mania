using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDashAttackExit : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player player = animator.GetComponentInParent<Player>();
        player.isInvincible = false;
        player.isDashing = false;
    }
}
