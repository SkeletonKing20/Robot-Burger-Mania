using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamagable
{
    protected int currentHp;
    protected int maxHp;
    protected bool isInvincible;
    public abstract void getHitForDamage(int damage, Transform attacker, int knockback);
    public virtual void getHitForDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHp -= damage;
            if (currentHp <= 0)
            {
                gameOver();
            }
            Vector2 direction = Vector2.right;
            StartCoroutine(InvincibilityCoroutine(1f, direction, 1));
        }
    }
    public virtual void gameOver()
    {

    }

    protected IEnumerator InvincibilityCoroutine(float duration, Vector2 direction, int knockback)
    {

        isInvincible = true;
        transform.Translate(direction * knockback);
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
