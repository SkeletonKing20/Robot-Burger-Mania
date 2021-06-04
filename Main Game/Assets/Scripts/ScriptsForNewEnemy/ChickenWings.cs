using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenWings : NewEnemyScript
{
    public Vector3[] PatrolPoints;
    public override IEnumerator IDLE()
    {
        while(states == ENEMY_STATE.IDLE && transform.position != PatrolPoints[1])
        {
            animator.transform.localScale = PatrolPoints[1].x >= transform.position.x ? scaleRight : scaleLeft;
            transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[1], deltaStep);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        while (states == ENEMY_STATE.IDLE && transform.position != PatrolPoints[0])
        {
            animator.transform.localScale = PatrolPoints[0].x <= transform.position.x ? scaleLeft : scaleRight;
            transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[0], deltaStep);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            ResetRoutine();
        }
    }
    public override IEnumerator CHASE()
    {
        lastPlayerPosition = player.transform.position;
        animator.transform.localScale = lastPlayerPosition.x > transform.position.x ? scaleRight : scaleLeft;
        animator.SetTrigger("startChasing");
        yield return new WaitForSeconds(.5f);
        while (transform.position != lastPlayerPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, lastPlayerPosition, deltaStep * 2);
            yield return null;
        }
        states = ENEMY_STATE.ATTACK;
    }

    public override IEnumerator DAMAGE()
    {
        Debug.Log("Ouch");
        isInvincible = true;
        animator.SetTrigger("TakeDamage");
        Vector3 targetKnockback = transform.position + knockBackTaken;
        while(transform.position != targetKnockback)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetKnockback, deltaStep * 3);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.5f);
        isInvincible = false;
        if(currentHp > 0) { states = ENEMY_STATE.IDLE; }
    }

    public override void DIE()
    {
        Debug.Log("I died");
        this.StopAllCoroutines();
        isInvincible = true;
        animator.SetTrigger("Die");
    }

    public override IEnumerator ATTACK()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.8f);
        states = ENEMY_STATE.IDLE;
    }
    public override IEnumerator EnemyFSM()
    {
        while (/*Mathf.Abs(transform.position.x - player.transform.position.x) < aggroRange*/true)
        {
            if (Random.value > .5 && transform.position.y > 4) { states = ENEMY_STATE.CHASE; }
            yield return StartCoroutine(states.ToString());
        }
    }

    public override void getHitByHeavyAttack()
    {

    }
}
