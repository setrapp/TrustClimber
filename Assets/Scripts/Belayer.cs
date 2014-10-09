using UnityEngine;
using System.Collections;

public class Belayer : MonoBehaviour {

	private bool pulling = false;
	private Vector3 destination = Vector3.zero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if(!pulling)
		{
			if(Input.GetButtonDown("RopeIn"))
			{		pulling = true;
					destination = transform.position + (Vector3.up * 2f);			
			}
			if(Input.GetMouseButtonDown(1))
				transform.position += Vector3.down;
		}
		else
		{
			if(destination != Vector3.zero)
			{
				//Debug.Log();
				if(Vector3.Magnitude(transform.position - destination) < .5f)
				{
					pulling = false;
					destination = Vector3.zero;
				}
			else
				transform.position = Vector3.Lerp(transform.position, destination, .05f);
			}	
		}
	}
}
