using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollect : MonoBehaviour
{
	public int amountOfItems;
	private ParticleSystem _particleSystem;
	private AudioSource itemsound;
	private Text _itemText;
	private Transform _door;

    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
		itemsound = GetComponent<AudioSource>();
		_itemText = GameObject.Find("ItemCollectedText").GetComponent<Text>();
		_itemText.text = amountOfItems + "/90";
		_door = GameObject.Find("Door").GetComponent<Transform>();
    }

	public void OnTriggerEnter(Collider Col){
		if(Col.gameObject.tag == "Item")
		{
			amountOfItems = amountOfItems + 1;
			Destroy(Col.gameObject);
			_particleSystem.Play();
			itemsound.Play();
			_itemText.text = amountOfItems + "/90";
			if (amountOfItems == 90) {_door.Translate(0, 10.0f, 0);}
			
		}
	}
		

    // Update is called once per frame
    void Update()
    {
        
    }
}
