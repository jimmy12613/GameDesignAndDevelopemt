//https://www.gameislearning.url.tw/article_content.php?getb=41&foog=9997
//https://answers.unity.com/questions/10443/how-to-rotate-an-objects-x-y-z-based-on-mouse-move.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	
	private CharacterController _controller;
	private Camera _cam;
	public float Speed = 2.0f;
	private float rotate = 2f;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
		/* if (Input.GetKey("a") ){　transform.Rotate( 0, -rotate* Time.deltaTime, 0 );　}
　　　　if (Input.GetKey("d") ){　transform.Rotate( 0, rotate* Time.deltaTime, 0 );　} */
        Vector3 move =  Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
		// Vector3 move =  Input.GetAxis("Vertical") * transform.forward;
		
		
		/* transform.Translate(move * Time.deltaTime * Speed); */
        _controller.Move(move * Time.deltaTime * Speed);
		
		float horizontalMove = rotate * Input.GetAxis("Mouse X");
        transform.Rotate(0, horizontalMove, 0);
    }
	
}
