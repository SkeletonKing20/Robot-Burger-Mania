using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnExit : StateMachineBehaviour
{
    [SerializeField]
    Sprite deathSprite;
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bigMech burger = animator.GetComponentInParent<bigMech>();
        BoxCollider2D boxC = burger.gameObject.GetComponentInParent<BoxCollider2D>();
        burger.gameObject.layer = 9;
        boxC.enabled = true;
        Destroy(burger);
    }
}
