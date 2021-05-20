using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public float JumpForce = 3;
    private Rigidbody2D _rigidbody;
    public float cooldown = 0.5f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Translate(-Vector2.right * speed * Time.deltaTime);
        
        if(Time.time > cooldown && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            cooldown = Time.time + 0.5f;
        }
    }

}
