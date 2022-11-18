using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearLevel : MonoBehaviour
{
    public GameObject clearLevelMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider Col){
		if(Col.gameObject.tag == "Player")
		{
            GameObject.Find("Timer").GetComponent<Timer>().finish(true);
		 	clearLevelMenu.SetActive(true);
		}
	}
}
