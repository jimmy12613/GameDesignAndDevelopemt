using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class frontMonster : AIPath
{
    public float attackRange = 1f;
    public float attackRate = 1f;
    public float attackCooldown = 0f;
    private float searchRange = 6f;
    public Transform front;
    public Transform player;

    Vector3 noTarget;

    public new void Start()
    {
        base.Start();
        noTarget = destination;
        player = GameObject.Find("Character").transform;
        front = GameObject.Find("t3").transform;
    }

    public new void Update()
    {
        base.Update();
        if (!destination.Equals(noTarget))
        {
            if (Vector3.Distance(transform.position, front.position) <= searchRange)
            {
                destination = player.position;
                if (Vector3.Distance(transform.position, player.position) <= attackRange)
                {
                    if (Time.time >= attackCooldown)
                    {
                        Attack();
                        attackCooldown = Time.time + 1f / attackRate;
                    }
                }
            }
            else
            {
                destination = front.position;
            }
        }
    }

    void Attack()
    {
        Debug.Log("Attack");
        GameObject.Find("Timer").GetComponent<Timer>().finish(false);
        GameObject.Find("Soldier").GetComponent<Animator>().SetInteger("Status", 1);
    }
}