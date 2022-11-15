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

    //Timer
    public Text timerText;
	public TimeSpan time;
    float startTime;
    public bool finished = false;

    void Start()
    {
        OnDisable();
    }
    void Update()
    {
        if (target != null && ai != null) ai.destination = target.position;
        if (!finished)
			{
				float t = Time.time - startTime;
        		time = TimeSpan.FromSeconds(Mathf.Floor(t* 100) / 100);
        		timerText.text = time.ToString("mm':'ss'.'ff");
			}
    }

    void InitiateTimer()
    {
        startTime = Time.time;
    }

    public void Finish()
    {
        finished = true;
    }

    public void OnEnable()
    {
        ai = GetComponent<IAstarAI>();
        InitiateTimer();
		if (ai != null) ai.onSearchPath += Update;
    }

    public void OnDisable()
    {
        if (ai != null) ai.onSearchPath -= Update;
    }
}
