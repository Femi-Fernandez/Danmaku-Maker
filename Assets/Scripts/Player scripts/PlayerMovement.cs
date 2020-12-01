using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	private float speed;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			speed = 3f;
		}
		else
		{
			speed = 6f;
		}
		transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) * speed * Time.deltaTime;
	}
}
