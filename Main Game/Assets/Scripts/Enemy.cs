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
    
    public virtual void Start()
    {
        player = FindObjectOfType<Player>();
        _rigidbody = GetComponent<Rigidbody2D>();
        animeThor = GetComponentInChildren<Animator>();
        spriteR = GetComponentInChildren<SpriteRenderer>();
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
                abandonShip();
            }
        }
    }

    public override void getHitForDamage(int damage, Transform attacker, int knockback)
    {
        if (!isInvincible)
        {
            currentHp -= damage;
            if (currentHp <= 0)
            {
                abandonShip();
            }
        }
    }

    public void TakeAHit(int damage)
    {
        animeThor.Play("Base Layer.tutBurgerHit", 0, 0);
        getHitForDamage(damage);
        player.hitConnect = true;
        Vector2 direction = (transform.position - player.transform.position).normalized.x * Vector2.right;
        StartCoroutine(InvincibilityCoroutine(1f, direction, 1));
    }

    public void abandonShip()
    {
        spriteR.flipX  = true;
        speed *= 2;
        isFleeing = true;
        isInvincible = true;
    }
}
