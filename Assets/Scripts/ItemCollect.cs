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

    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
		itemsound = GetComponent<AudioSource>();
		_itemText = GameObject.Find("ItemCollectedText").GetComponent<Text>();
		_itemText.text = amountOfItems + "/90";
    }

	public void OnTriggerEnter(Collider Col){
		if(Col.gameObject.tag == "Item")
		{
			amountOfItems = amountOfItems + 1;
			Destroy(Col.gameObject);
			_particleSystem.Play();
			itemsound.Play();
			_itemText.text = amountOfItems + "/90";
		}
	
	}
		

    // Update is called once per frame
    void Update()
    {
        
    }
}
