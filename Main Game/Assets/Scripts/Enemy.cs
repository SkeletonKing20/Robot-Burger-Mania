using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, IDamagable
{
    public float speed = 5f;
    public float JumpForce = 3;
    private Rigidbody2D _rigidbody;
    private Player player;
    public float cooldown = 0.5f;
    public int damage; 
    bool hopping = true;
    Animator animeThor;
    bool isInvincible;
    SpriteRenderer spriteR;
    bool isFleeing;
    void Start()
    {
        player = FindObjectOfType<Player>();
        _rigidbody = GetComponent<Rigidbody2D>();
        animeThor = GetComponentInChildren<Animator>();
        spriteR = GetComponentInChildren<SpriteRenderer>();
        maxHp = 3;
        currentHp = maxHp;
        knockback = 1f;
    }

    void Update()
    {
        Jump();
        if (hopping && !isFleeing)
        {

            Debug.Log((getDistanceFromObject(player.gameObject) < 3));
            if((getDistanceFromObject(player.gameObject) > 3))
            {
                if(player.transform.position.x < transform.position.x)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack(damage);
        }
        if(collision.gameObject.CompareTag("Attack"))
        {
            getHitForDamage(1);
        }
    }

    public void Attack(int damage)
    {
        player.getHitForDamage(damage);
    }

    public override void getHitForDamage(int damage)
    {
        if (!isInvincible)
        {
            animeThor.Play("Base Layer.tutBurgerHit", 0, 0);
            currentHp--;
            if (currentHp <= 0)
            {
                abandonShip();
            }
            player.hitConnect = true;
            StartCoroutine(InvincibilityCoroutine(1f));
        }
        
    }

    public void abandonShip()
    {
        spriteR.flipX  = true;
        speed *= 2;
        isFleeing = true;
        isInvincible = true;
    }
}
