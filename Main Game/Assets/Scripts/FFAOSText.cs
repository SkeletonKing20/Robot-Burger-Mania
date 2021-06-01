using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFAOSText : MonoBehaviour
{
    AutoText autotext;
    public TMPro.TMP_Text textObject;
    void Start()
    {
        autotext = GetComponentInParent<AutoText>();
    }

    public void WriteCurrentText(string currentText)
    {
        autotext.Run(currentText, textObject);
    }
}
