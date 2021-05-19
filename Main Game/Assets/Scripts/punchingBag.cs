using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punchingBag : MonoBehaviour
{
    float knockback  = 1f;
    public void TakeAHit()
    {
        transform.position += Vector3.right * knockback;
    }
}
