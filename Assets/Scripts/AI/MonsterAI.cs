using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonsterAI : AIPath
{
    public float attackRange = 0.1f;
    public float attackRate = 1f;
    public float attackCooldown = 0f;

    Vector3 noTarget;

    public new void Start()
    {
        base.Start();
        noTarget = destination;
    }

    public new void Update()
    {
        base.Update();
        print("Player position: " + destination);
        print("Monster position: " + transform.position);
        if (!destination.Equals(noTarget))
        {
            if (Vector3.Distance(transform.position, destination) <= attackRange)
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
        print("Player position: " + destination);
        print("Monster position: " + transform.position);
        print(Vector3.Distance(transform.position, destination));
        Debug.Log("Attack");
        GameObject.Find("Timer").GetComponent<Timer>().finish(false);
        GameObject.Find("Soldier").GetComponent<Animator>().SetInteger("Status", 1);
    }
}