using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IDamagable
{
    void getHitForDamage(int damage);
    void getHitForDamage(int damage, Transform attacker, float knockback);
}
