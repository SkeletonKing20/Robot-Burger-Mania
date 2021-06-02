using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologScript : StateMachineBehaviour
{
    public FFAOSText text;
    string currentText = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //text.WriteCurrentText(currentText);
    }
}
