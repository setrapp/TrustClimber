using UnityEngine;
using System.Collections;

public class ClimbInput : MonoBehaviour {
	public GameObject leftHand;
	public GameObject rightHand;
	private bool movingLeftHand = true;
	private float bodyBelowHands;

	void Awake()
	{
		bodyBelowHands = leftHand.transform.localPosition.y;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (movingLeftHand)
			{
				leftHand.transform.position += new Vector3(0, 1, 0);
			}
			else
			{
				rightHand.transform.position += new Vector3(0, 1, 0);
			}

			movingLeftHand = !movingLeftHand;
			Vector3 newPosition = (leftHand.transform.position + rightHand.transform.position) / 2;
			newPosition.y -= bodyBelowHands;
			leftHand.transform.position -= (newPosition - transform.position);
			rightHand.transform.position -= (newPosition - transform.position);
			transform.position = newPosition;
		}
	}
}
