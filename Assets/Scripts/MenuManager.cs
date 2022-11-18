using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        // target = GameObject.Find("OptionsMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            target.SetActive(!target.activeSelf);
            Debug.Log("Escape key Down");
        }

        // if(Input.GetKeyUp(KeyCode.Escape))
        // {
        //     target.SetActive(false);
        //     Debug.Log("Escape key Up");
        // }
    }
}
