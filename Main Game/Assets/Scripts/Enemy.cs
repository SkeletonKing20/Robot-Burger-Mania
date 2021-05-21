using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public float speed = 5f;
    public float JumpForce = 3;
    private Rigidbody2D _rigidbody;
    private Player player;
    public float cooldown = 0.5f;
    public int damage;
    public float knockback = 1f;
    bool hopping = true;
    Animator animeThor;
    bool isInvincible;

    int currentHp;
    int maxHp;
    bool isFleeing;
    void Start()
    {
        player = FindObjectOfType<Player>();
        _rigidbody = GetComponent<Rigidbody2D>();
        animeThor = GetComponentInChildren<Animator>();
        maxHp = 3;
        currentHp = maxHp;
    }

    void Update()
    {
        Jump();
        if (hopping && !isFleeing)
        {

            Debug.Log((getDistanceFromObject(player) < 3));
            if((getDistanceFromObject(player) > 3))
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

            if ((getDistanceFromObject(player) < 3) || getDistanceFromObject(player) == 0)
            {
                    moveRight();
            
            }
        }

        if (isFleeing && getDistanceFromObject(player) < 12f)
        {
            moveRight();
        }
        else
        {
            Jump();
        }
    }

    public float getDistanceFromObject(Player obj)
    {
        return Mathf.Abs((obj.transform.position.x) - transform.position.x);
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
            TakeAHit();
        }
    }

    public void Attack(int damage)
    {
        player.getHitForDamage(damage);
    }

    public void TakeAHit()
    {
        if (!isInvincible)
        {
            currentHp--;
            if (currentHp <= 0)
            {
                abandonShip();
            }
            player.hitConnect = true;
            StartCoroutine(InvincibilityCoroutine(1f));
        }
        
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        
        isInvincible = true;
        animeThor.Play("Base Layer.tutBurgerHit", 0, 0);
        transform.Translate(Vector3.right * knockback);
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
        }
        isInvincible = false;
    }

    public void abandonShip()
    {
        speed *= 2;
        isFleeing = true;
    }
}
