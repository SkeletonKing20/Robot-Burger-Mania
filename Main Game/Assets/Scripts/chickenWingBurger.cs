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

    float initialHeight;
    public override void Start()
    {
        base.Start();
        diveStep = speed * Time.deltaTime;
        currentPosition = transform.position;
        cooldown = 5f;
        initialHeight = currentPosition.y;
    }

    private void Update()
    {
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
            currentPosition = transform.position;
        }
    }

    public void returnToHeight(float height)
    {
        Vector3 target = new Vector3(transform.position.x, height, 0);
        while(transform.position.y < height)
        Vector3.MoveTowards(transform.position,target, diveStep);
    }
    public void Dive()
    {
        diveStep = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, diveStep);
        if(Mathf.Abs(transform.position.x - targetPosition.x) < 0.000001f)
        {
            isDiving = false;
            returnToHeight(initialHeight);
        }
    }
}
