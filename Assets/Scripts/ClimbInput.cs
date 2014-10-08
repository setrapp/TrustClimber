using UnityEngine;
using System.Collections;

public class ClimbInput : MonoBehaviour {
	public GameObject leftHand;
	public GameObject rightHand;
	private bool movingLeftHand = true;
	private float bodyBelowHands;
	public float maxArmDistance;

	void Awake()
	{
		bodyBelowHands = leftHand.transform.localPosition.y;
	}

	void Update()
	{
		Handhold nextHandhold = null;
		Vector3 handPos = leftHand.transform.position;
		if (!movingLeftHand)
		{
			handPos = rightHand.transform.position;
		}

		if (Input.GetButtonDown("Top"))
		{
			nextHandhold = HandholdManager.Instance.NearestHandhold(Handhold.ButtonType.Top, handPos, transform.position.y);
		}
		else if (Input.GetButtonDown("Left"))
		{
			nextHandhold = HandholdManager.Instance.NearestHandhold(Handhold.ButtonType.Left, handPos, transform.position.y);
		}
		else if (Input.GetButtonDown("Bottom"))
		{
			nextHandhold = HandholdManager.Instance.NearestHandhold(Handhold.ButtonType.Bottom, handPos, transform.position.y);
		}
		else if (Input.GetButtonDown("Right"))
		{
			nextHandhold = HandholdManager.Instance.NearestHandhold(Handhold.ButtonType.Right, handPos, transform.position.y);
		}

		if (nextHandhold != null && (nextHandhold.transform.position - transform.position).sqrMagnitude <= Mathf.Pow(maxArmDistance, 2))
		{
			if (movingLeftHand)
			{
				leftHand.transform.position = nextHandhold.transform.position;
			}
			else
			{
				rightHand.transform.position = nextHandhold.transform.position;
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
