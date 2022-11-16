using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    bool isFinish;
    public Text timerText;
    public TimeSpan time;
    float startTime;

    PlayfabManager playfabManager;

    // Start is called before the first frame update
    void Start()
    {
        playfabManager = GetComponent<PlayfabManager>();
        isFinish = Singleton.Instance.getIsFinish();
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
        Singleton.Instance.setIsFinish(false);
        playfabManager.GetPlayerLevelStatus();
    }

    public void finish()
    {
        isFinish = true;
        GameObject.Find("MonsterAI").GetComponent<MonsterAI>().enabled = false;

        playfabManager.uploadLevel1Score(time.TotalSeconds);
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            Singleton.Instance.setLevel1Status(true);
        } else if (SceneManager.GetActiveScene().name == "Level2")
        {
            Singleton.Instance.setLevel2Status(true);
        }
        playfabManager.SavePlayerLevelStatus();
    }
}
