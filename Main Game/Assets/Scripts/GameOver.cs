using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : StateMachineBehaviour
{
    Player player;
    MonoBehaviour monoBehave;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monoBehave = animator.GetComponentInParent<MonoBehaviour>();
        monoBehave.StartCoroutine(GameOverScreen(stateInfo.length));
    }

    IEnumerator GameOverScreen(float duration)
    {
        yield return new WaitForSeconds(duration);
    }
}
