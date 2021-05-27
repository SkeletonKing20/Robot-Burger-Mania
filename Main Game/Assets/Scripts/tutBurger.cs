using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutBurger : Enemy
{
    public override void Start()
    {
        base.Start();
        maxHp = 3;
        currentHp = maxHp;
        knockback = 5;
    }

    public virtual void Update()
    {
        Jump();
        if (hopping && !isFleeing)
        {

            if ((getDistanceFromObject(player.gameObject) > 3))
            {
                if (player.transform.position.x < transform.position.x)
                {
                    moveLeft();
                }
                else
                {
                    moveRight();
                }
            }

            if ((getDistanceFromObject(player.gameObject) < 3) || getDistanceFromObject(player.gameObject) == 0)
            {
                moveRight();

            }
        }

        if (isFleeing && getDistanceFromObject(player.gameObject) < 12f)
        {
            moveRight();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack(damage);
        }
        if (collision.gameObject.CompareTag("Attack"))
        {
            TakeAHit(1);
        }
        if (collision.gameObject.CompareTag("downTilt"))
        {
            animeThor.Play("Base Layer.tutBurgerHit", 0, 0);
            getHitForDamage(1);
            hopping = false;
            _rigidbody.AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack(damage);
        }
    }
}
