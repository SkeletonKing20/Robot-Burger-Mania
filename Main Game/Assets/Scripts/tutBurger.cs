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

    public void abandonShip()
    {
        spriteR.flipX = true;
        speed *= 2;
        isFleeing = true;
        isInvincible = true;
    }

    public override void gameOver()
    {
        abandonShip();
    }
}
