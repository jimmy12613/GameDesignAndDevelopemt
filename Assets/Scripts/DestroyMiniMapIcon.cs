using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMiniMapIcon : MonoBehaviour
{
    public GameObject key;
    public GameObject keyIcon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // key = GameObject.Find("Key_01");
        if(key == null){
            keyIcon.SetActive(false);
        }
    }
}
