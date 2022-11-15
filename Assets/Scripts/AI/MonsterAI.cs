using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonsterAI : AIPath
{
    public float attackRange = 0.1f;
    public float attackRate = 1f;
    public float attackCooldown = 0f;

    public new void Start()
    {
        base.Start();
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
        GameObject.Find("Timer").GetComponent<Timer>().finish();
    }
}