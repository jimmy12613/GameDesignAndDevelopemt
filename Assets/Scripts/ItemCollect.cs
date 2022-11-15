using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
	public int amountOfItems;
	private ParticleSystem _particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

	public void OnTriggerEnter(Collider Col){
		if(Col.gameObject.tag == "Item")
		{
			amountOfItems = amountOfItems + 1;
			Destroy(Col.gameObject);
			_particleSystem.Play();
		}
	
	}
		

    // Update is called once per frame
    void Update()
    {
        
    }
}
