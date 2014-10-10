using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	public bool finished = false;
	public GameObject climberOne;
	public GameObject climberTwo;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if(climberOne.GetComponent<ClimbInput>().safe && climberTwo.GetComponent<ClimbInput>().safe)
		{
			finished = true;
			print ("Finished");
		}
		if(finished == true)
		{
			climberOne.SendMessage("VictHands",SendMessageOptions.DontRequireReceiver);
			climberTwo.SendMessage("VictHands",SendMessageOptions.DontRequireReceiver);
		}
	
	}

}
