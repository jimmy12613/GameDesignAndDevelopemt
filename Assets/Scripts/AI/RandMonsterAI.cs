using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RandMonsterAI : AIPath
{
    public float attackRange = 1.5f;
    public float attackRate = 1f;
    public float attackCooldown = 0f;
    public Transform player;

    Vector3 noTarget;

    public new void Start()
    {
        base.Start();
        noTarget = destination;
        player = GameObject.Find("Character").transform;
    }

    public new void Update()
    {
        base.Update();
        if (!destination.Equals(noTarget))
        {
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
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
        GameObject.Find("Timer").GetComponent<Timer>().finish(false);
        
    }
}