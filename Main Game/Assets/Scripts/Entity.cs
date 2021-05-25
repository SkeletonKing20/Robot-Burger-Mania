using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamagable
{
    protected int currentHp;
    protected int maxHp;
    protected float knockback;
    protected bool isInvincible;
    public virtual void getHitForDamage(int damage)
    {
        currentHp -= 1;
        if (currentHp <= 0)
        {
            gameOver();
        }
    }

    public virtual void gameOver()
    {

    }

    protected IEnumerator InvincibilityCoroutine(float duration)
    {

        isInvincible = true;
        transform.Translate(Vector3.right * knockback);
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
        }
        isInvincible = false;
    }

    public float getDistanceFromObject(GameObject obj)
    {
        return Mathf.Abs((obj.transform.position.x) - transform.position.x);
    }
}
