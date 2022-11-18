using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollect : MonoBehaviour
{
	public int itemCollected;
	private ParticleSystem _particleSystem;
	public AudioSource itemsound;
	private Text _itemText;
	private int _totalItem;

    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
		
		_itemText = GameObject.Find("ItemCollectedText").GetComponent<Text>();
		_itemText.text = itemCollected + "/90";

		_totalItem = GameObject.Find("Items").GetComponent<Transform>().childCount;
		// Debug.Log(_totalItem);
    }

	public void OnTriggerEnter(Collider Col){
		if(Col.gameObject.tag == "Item")
		{
		 	itemCollected++;
			Destroy(Col.gameObject);
			_particleSystem.Play();
			itemsound.Play();
			_itemText.text = itemCollected + "/" + _totalItem;
		}
	}
		

    // Update is called once per frame
    void Update()
    {
        _itemText.text = itemCollected + "/" + _totalItem;
    }
}
