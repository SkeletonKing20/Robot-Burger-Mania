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

    public bool isDiving;
    public bool isSitting;

    const float initialHeight = 2;
    public override void Start()
    {
        base.Start();
        diveStep = speed * Time.deltaTime;
        currentPosition = transform.position;
        cooldown = 5f;
        knockback = 1;
    }

    private void Update()
    {
        returnToHeight(initialHeight);
        if (!isSitting)
        {
            if(Time.time > cooldown)
            {
                isDiving = true;
                targetPosition = player.transform.position;
                cooldown = Time.time + 5f;
            }
            if(isDiving)
            {
                Dive();
            }
            if (!isDiving)
            {
                returnToHeight(initialHeight);
            }
            currentPosition = transform.position;
        }
    }

    public void returnToHeight(float height)
    {
        Vector3 target = new Vector3(currentPosition.x, height, 0);
        if(transform.position.y < height)
        {
            Vector3.MoveTowards(currentPosition,target, diveStep);
        }
    }
    public void Dive()
    {
        diveStep = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, diveStep);
        if(Mathf.Abs(transform.position.x - targetPosition.x) < 0.0001f)
        {
            isDiving = false;
        }
    }
}
