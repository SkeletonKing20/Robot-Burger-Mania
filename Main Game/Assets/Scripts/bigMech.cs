using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigMech : Enemy
{
    public bool isAttacking;
    float biteCoolDown;
    public override void Start()
    {
        initialKnockbackTaken = 1;
        base.Start();
        maxHp = 10;
        currentHp = maxHp;
        knockback = 3;
        damage = 2;
        speed = 1;
    }
    public void Update()
    {
        player = FindObjectOfType<Player>();
        
         
            if (!isAttacking)
            {
                if (Time.time > cooldown && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
                {
                    _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                    cooldown = Time.time + 0.5f;

                }
                if (player.transform.position.x < transform.position.x)
                {
                    animeThor.transform.localScale = scaleRight;
                    moveLeft();
                }
                else
                {
                    animeThor.transform.localScale = scaleLeft;
                    moveRight();
                }
            }
            if (Time.time > biteCoolDown)
            {
                isAttacking = true;
                Bite();
                biteCoolDown = Time.time + 3f;
            }
        
    }

    public void Bite()
    {
        if(animeThor != null)
        {
            animeThor.SetTrigger("Attack");
        }
    }

    public override void gameOver()
    {
        isInvincible = true;
        isDead = true;
        animeThor.SetTrigger("Die");
    }

    public IEnumerator MoveTo(Vector3 target)
    {
        while(Mathf.Abs(transform.position.x - target.x) > 0.0000001f && Mathf.Abs(transform.position.x - target.x) < 10)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, transform.position.y, 0), 5f * Time.deltaTime);
            yield return null;
        }
    }

    public void attackPosition(Vector3 target)
    {
        StartCoroutine(MoveTo(target));
    }


}