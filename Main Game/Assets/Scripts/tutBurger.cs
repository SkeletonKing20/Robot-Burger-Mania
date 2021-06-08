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
        knockback = 1;
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

        if (isFleeing && getDistanceFromObject(player.gameObject) < 22f)
        {
            moveRight();
        }


    }

    public void abandonShip()
    {
        spriteR.flipX = true;
        speed *= 4;
        isFleeing = true;
    }

    public override void gameOver()
    {
        abandonShip();
    }
}
