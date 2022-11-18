using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChange : MonoBehaviour
{
    private CinemachineVirtualCamera _cam;
    // Start is called before the first frame update
    void Start()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            _cam.Priority = 11;
        }
        if (Input.GetKeyUp("space"))
        {
             _cam.Priority = 9;
        }
    }
}
