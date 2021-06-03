using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, IDamagable
{
    public float speed = 5f;
    public float JumpForce = 3;
    protected Rigidbody2D _rigidbody;
    protected Player player;
    public float cooldown = 0.5f;
    public int damage; 
    protected bool hopping = true;
    protected Animator animeThor;
    protected SpriteRenderer spriteR;
    protected bool isFleeing;
    protected int knockback;
    protected bool isDead;
    public float knockbackTaken;
    public float initialKnockbackTaken = 1;
    public float aggroRange;
    public virtual void Start()
    {
        player = FindObjectOfType<Player>();
        _rigidbody = GetComponent<Rigidbody2D>();
        animeThor = GetComponentInChildren<Animator>();
        spriteR = GetComponentInChildren<SpriteRenderer>();
        knockbackTaken = initialKnockbackTaken;
        transform.position = startingPosition;
    }
    public void moveRight()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public void moveLeft()
    {
        transform.Translate(-Vector2.right * speed * Time.deltaTime);
    }

    public void Jump()
    {
        if (Time.time > cooldown && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            float randomJump = Random.Range(0, 3);
            Debug.Log(randomJump);
            if (randomJump == 0) 
            {
                hopping = false;
                _rigidbody.AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
                cooldown = Time.time + 0.5f;
            }
            else
            {
                hopping = true;
                _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                cooldown = Time.time + 0.5f;
            }

        }
    }

    
public virtual void Attack(int damage)
    {
        player.getHitForDamage(damage, this.transform, knockback);
    }

    public override void getHitForDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHp -= damage;
            if (currentHp <= 0)
            {
                gameOver();
            }
        }
    }

    public override void getHitForDamage(int damage, Transform attacker, float knockback)
    {
        if (!isInvincible)
        {
            currentHp -= damage;
            if (currentHp <= 0)
            {
                gameOver();
            }
        }
    }

    public void TakeAHit(int damage)
    {
        animeThor?.SetTrigger("TakeDamage");
        getHitForDamage(damage);
        player.hitConnect = true;
        Vector2 direction = (transform.position - player.transform.position).normalized.x * Vector2.right;
        StartCoroutine(InvincibilityCoroutine(1f, direction, knockbackTaken));
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isInvincible)
        {

            if (collision.gameObject.CompareTag("Player"))
            {
                Attack(damage);
            }
            if (collision.gameObject.CompareTag("Attack"))
            {
                knockbackTaken = 0;
                TakeAHit(1);
            }
            if (collision.gameObject.CompareTag("heavyAttack"))
            {
                knockbackTaken = initialKnockbackTaken * 2;
                TakeAHit(2);
            }
            if (collision.gameObject.CompareTag("downTilt"))
            {
                animeThor.SetTrigger("TakeDamage");
                getHitForDamage(1);
                hopping = false;
                _rigidbody.AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack(damage);
        }
    }

    public override void gameOver()
    {
        isInvincible = true;
        isDead = true;
        animeThor.SetTrigger("Die");     
    }
}
