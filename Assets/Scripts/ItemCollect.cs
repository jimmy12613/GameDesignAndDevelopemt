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
	private CharacterMovement _characterMovement;
	private Animator _animator;
	private bool _isSprint;
	private float _startTime;
	private float _currentSpeed;
	private float _currentAniSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
		
		_itemText = GameObject.Find("ItemCollectedText").GetComponent<Text>();
		_itemText.text = itemCollected + "/90";

		_totalItem = GameObject.Find("Items").GetComponent<Transform>().childCount;
		// Debug.Log(_totalItem);

		_characterMovement = GetComponent<CharacterMovement>();
		_animator = GetComponent<Animator>();
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

		if (Col.gameObject.tag == "Potion")
		{
			Destroy(Col.gameObject);
			_particleSystem.Play();
			itemsound.Play();
			Sprint();
		}
	}
		

    // Update is called once per frame
    void Update()
    {
        _itemText.text = itemCollected + "/" + _totalItem;
		if ((Time.time - _startTime > 2.0f) && _isSprint)
		{
			_isSprint = false;
			_characterMovement.Speed = _currentSpeed;
			_animator.speed = _currentAniSpeed;
		}
    }

	void Sprint()
	{
		_isSprint = true;
		_startTime = Time.time;
		_currentSpeed = _characterMovement.Speed;
		_characterMovement.Speed = _currentSpeed * 2.0f;
		_currentAniSpeed = _animator.speed;
		_animator.speed = _currentAniSpeed * 1.5f;
	}
}
