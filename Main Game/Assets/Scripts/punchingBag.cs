using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punchingBag : MonoBehaviour, IDamagable
{
    float knockback  = 1f;
    public void TakeAHit()
    {
        transform.Translate(Vector3.right * knockback);
    }
}
