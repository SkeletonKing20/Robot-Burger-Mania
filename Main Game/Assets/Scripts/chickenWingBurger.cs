using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickenWingBurger : Enemy
{
    Vector3 currentPosition;
    Vector3 targetPosition;

    float currentAttackCoolDown;
    float attackCoolDown;

    float diveStep;

    bool isDiving;
    public bool isSitting;

    public override void Start()
    {
        base.Start();
        diveStep = speed * Time.deltaTime;
    }

    private void Update()
    {
        if (!isSitting)
        {
            if (Time.time > currentAttackCoolDown && !isDiving)
            {
                isDiving = true;
                targetPosition = player.transform.position;
                currentAttackCoolDown = Time.time + attackCoolDown;
            }
            if(isDiving)
            {
                Dive();
            }
            currentPosition = transform.position;
        }
    }

    public void Dive()
    {
        diveStep = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, diveStep);
    }
}
