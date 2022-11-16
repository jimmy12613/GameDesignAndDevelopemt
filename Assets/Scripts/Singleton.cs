using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }
    
    public bool isFinish = false;
    public bool trace = false;
    public bool level1Status = false;
    public bool level2Status = false;

    private void Awake() 
    { 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //get set method of isFinish
    public bool getIsFinish()
    {
        return isFinish;
    }
    public void setIsFinish(bool isFinish)
    {
        this.isFinish = isFinish;
    }

    //get set method of trace
    public bool getTrace()
    {
        return trace;
    }
    public void setTrace(bool trace)
    {
        this.trace = trace;
    }

    //get set method of level1Status
    public bool getLevel1Status()
    {
        return level1Status;
    }
    public void setLevel1Status(bool level1Status)
    {
        this.level1Status = level1Status;
    }

    //get set method of level2Status
    public bool getLevel2Status()
    {
        return level2Status;
    }
    public void setLevel2Status(bool level2Status)
    {
        this.level2Status = level2Status;
    }
}
