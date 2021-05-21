using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public float JumpForce = 3;
    private Rigidbody2D _rigidbody;
    private Player player;
    public float cooldown = 0.5f;
    void Start()
    {
        player = FindObjectOfType<Player>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Jump();
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
            if (Random.Range(0,10) == 0) 
            {
                _rigidbody.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
                cooldown = Time.time + 0.5f;
            }
            else
            {
                _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                cooldown = Time.time + 0.5f;
            }

        }
    }
}
