using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    private Transform _doorPosition;
    private ItemCollect _itemCollectScript;
    private int _itemCollected;
    private int _totalItem;
    private float _doorMoveHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        _doorPosition = GetComponent<Transform>();
        _itemCollectScript = GameObject.Find("Character").GetComponent<ItemCollect>();
        _totalItem = GameObject.Find("Items").GetComponent<Transform>().childCount;
        _doorMoveHeight = _doorPosition.position.y + 15;
    }

    // Update is called once per frame
    void Update()
    {
        _itemCollected = _itemCollectScript.itemCollected;
        if (_itemCollected > 0 && _doorPosition.position.y < _doorMoveHeight) {
            _doorPosition.Translate(0, 0.01f, 0);
        }
        // if (_itemCollected == _totalItem && _doorPosition.position.y < _doorMoveHeight) {
        //     _doorPosition.Translate(0, 10.0f, 0);
        // }
    }
}
