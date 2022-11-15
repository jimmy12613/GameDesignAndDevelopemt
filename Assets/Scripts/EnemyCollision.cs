using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	public void OnTriggerEnter(Collider Col){
		if(Col.gameObject.tag == "Enemy")
		{
			Debug.Log("enemy touch");
		}
	
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
