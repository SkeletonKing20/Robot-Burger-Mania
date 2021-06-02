using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamagable
{
    public float currentHp;
    public float maxHp;
    public bool isInvincible;
    protected Vector3 scaleLeft = new Vector3(-1f, 1f, 1);
    protected Vector3 scaleRight = new Vector3(1f, 1f, 1);
    public abstract void getHitForDamage(int damage, Transform attacker, float knockback);
    public virtual void getHitForDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHp -= damage;
            checkForDeath();
            Vector2 direction = Vector2.right;
            StartCoroutine(InvincibilityCoroutine(1f, direction, 1f));
        }
    }
    public abstract void gameOver();

    public virtual IEnumerator InvincibilityCoroutine(float duration, Vector2 direction, float knockback)
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
    public bool checkForDeath()
    {
        if (currentHp <= 0)
        {
            return true;
        }
        return false;
    }
}
