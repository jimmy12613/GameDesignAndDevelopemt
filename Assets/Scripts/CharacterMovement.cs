//https://www.gameislearning.url.tw/article_content.php?getb=41&foog=9997
//https://answers.unity.com/questions/10443/how-to-rotate-an-objects-x-y-z-based-on-mouse-move.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	
	private CharacterController _controller;
    private Animator _animator;
	public float Speed = 2.0f;
    public float rotationSpeed;
	private float rotate = 2f;
    private Timer _timer;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _timer = GameObject.Find("Timer").GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
		/* if (Input.GetKey("a") ){　transform.Rotate( 0, -rotate* Time.deltaTime, 0 );　}
　　　　if (Input.GetKey("d") ){　transform.Rotate( 0, rotate* Time.deltaTime, 0 );　} */
        Vector3 move =  Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
		// Vector3 move =  Input.GetAxis("Vertical") * transform.forward;
		
		
		/* transform.Translate(move * Time.deltaTime * Speed); */
        if (!_timer.isFinish){
            _controller.Move(move * Time.deltaTime * Speed);
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
		
		    float horizontalMove = rotate * Input.GetAxis("Mouse X");
            transform.Rotate(0, horizontalMove, 0);
        }

        //animation
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        if (move != Vector3.zero)
        {
            _animator.SetBool("isRunning", true);
            // Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            // transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
        
    }
	
}
