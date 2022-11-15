using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonsterAI : AIPath
{
    public float attackRange = 1f;
    public float attackRate = 1f;
    public float attackCooldown = 0f;

    public new Transform target;

    public new void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public new void Update()
    {
        base.Update();
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) <= attackRange)
            {
                if (Time.time >= attackCooldown)
                {
                    Attack();
                    attackCooldown = Time.time + 1f / attackRate;
                }
            }
        }
    }

    void Attack()
    {
        Debug.Log("Attack");
    }
}