using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigMech : Enemy
{
    public override void Start()
    {
        base.Start();
        maxHp = 10;
        currentHp = maxHp;
        knockback = 10;
        damage = 2;
    }
    public void Update()
    {
        Jump();
        if (Time.time > cooldown)
        {
            Bite();
            cooldown = Time.time + 5f;
        }
    }

    public void Bite()
    {
        animeThor.SetTrigger("Attack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack(damage);
        }
    }
}