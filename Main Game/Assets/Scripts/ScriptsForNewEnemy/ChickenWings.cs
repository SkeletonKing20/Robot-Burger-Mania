using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenWings : NewEnemyScript
{
    public Vector3[] PatrolPoints;
    public override IEnumerator IDLE()
    {
        animator.transform.localScale = PatrolPoints[1].x > transform.position.x ? scaleRight : scaleLeft;
        while(states == ENEMY_STATE.IDLE && transform.position != PatrolPoints[1])
        {
            transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[1], deltaStep);
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        animator.transform.localScale = PatrolPoints[1].x > transform.position.x ? scaleRight : scaleLeft;
        while (states == ENEMY_STATE.IDLE && transform.position != PatrolPoints[0])
        {
            transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[0], deltaStep);
            yield return null;
        }
        yield return new WaitForSeconds(5f);
    }

    public override IEnumerator CHASE()
    {
        lastPlayerPosition = player.transform.position;
        while (transform.position != lastPlayerPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, lastPlayerPosition, deltaStep * 2);
            yield return null;
        }
        states = ENEMY_STATE.IDLE;
    }

    public override IEnumerator DAMAGE()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator DIE()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator ATTACK()
    {
        throw new System.NotImplementedException();
    }
}
