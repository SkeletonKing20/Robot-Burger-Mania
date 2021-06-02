using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnExit : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.GetComponentInParent<chickenWingBurger>().gameObject , stateInfo.length);
    }
}
