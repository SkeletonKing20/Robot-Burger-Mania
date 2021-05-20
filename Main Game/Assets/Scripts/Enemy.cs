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
    private bool moveLeft = false;
    private bool moveRight = false;

    void Start()
    {
        player = FindObjectOfType<Player>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player.transform.position.x - transform.position.x  >= -3 && player.transform.position.x - transform.position.x < -2)
        {
            moveLeft = false;
            moveRight = true;
        }

        if (player.transform.position.x - transform.position.x <= -7 && player.transform.position.x - transform.position.x > -10)
        {
            moveRight = false;
            moveLeft = true;
        }

        if (player.transform.position.x - transform.position.x  <= 3 && player.transform.position.x - transform.position.x > 2)
        {
            moveRight = false;
            moveLeft = true;
        }

        if (player.transform.position.x - transform.position.x  >= 7 && player.transform.position.x - transform.position.x <= 8)
        {
            moveLeft = false;
            moveRight = true;
        }


        if (moveRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        if (moveLeft)
        {
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
        }


        if (Time.time > cooldown && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            cooldown = Time.time + 0.5f;
        }
    }

}
