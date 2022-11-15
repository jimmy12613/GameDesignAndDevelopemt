using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }
    
    public bool isFinish = false;
    public bool trace = false;

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
}
