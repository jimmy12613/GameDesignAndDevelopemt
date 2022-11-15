using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    bool isFinish = Singleton.Instance.isFinish;
    public Text timerText;
    public TimeSpan time;
    float startTime;

    PlayfabManager playfabManager;

    // Start is called before the first frame update
    void Start()
    {
        playfabManager = GetComponent<PlayfabManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFinish)
		{
			float t = Time.time - startTime;
        	time = TimeSpan.FromSeconds(Mathf.Floor(t* 100) / 100);
        	timerText.text = time.ToString("mm':'ss'.'ff");
		}
    }

    public void initTimer()
    {
        startTime = Time.time;
    }

    public void finish()
    {
        isFinish = true;
        playfabManager.uploadLevel1Score(time.TotalSeconds);
    }
}
