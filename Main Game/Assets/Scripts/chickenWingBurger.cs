using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickenWingBurger : Enemy
{
    Vector3 currentPosition;
    Vector3 targetPosition;

    float currentAttackCoolDown;
    float attackCoolDown;

    bool isDiving;
    public void enableDive()
    {
        isDiving = true;
        currentPosition = transform.position;
        targetPosition = player.transform.position;
    }

    private void Update()
    {
        if (Time.time > currentAttackCoolDown && !isDiving)
        {
            enableDive();
            currentAttackCoolDown = Time.time + attackCoolDown;
        }
        if(isDiving)
        {
            Dive();
        }
    }

    public void Dive()
    {

    }
}
