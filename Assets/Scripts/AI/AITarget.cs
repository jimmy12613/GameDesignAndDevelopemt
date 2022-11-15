using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Pathfinding;
using System;

public class AITarget : AIDestinationSetter
{
    //AI
    IAstarAI ai;

    void Start()
    {
        OnDisable();
    }

    void Update()
    {   
        if (target != null && ai != null) ai.destination = target.position;
    }

    public void OnEnable()
    {
        ai = GetComponent<IAstarAI>();
		if (ai != null) ai.onSearchPath += Update;
    }

    public void OnDisable()
    {
        if (ai != null) ai.onSearchPath -= Update;
    }
}
