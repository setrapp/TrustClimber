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

		if (Input.GetKeyDown("w"))
		{
			nextHandhold = HandholdManager.Instance.NearestHandhold(Handhold.ButtonType.W, handPos, transform.position.y);
		}
		else if (Input.GetKeyDown("a"))
		{
			nextHandhold = HandholdManager.Instance.NearestHandhold(Handhold.ButtonType.A, handPos, transform.position.y);
		}
		else if (Input.GetKeyDown("s"))
		{
			nextHandhold = HandholdManager.Instance.NearestHandhold(Handhold.ButtonType.S, handPos, transform.position.y);
		}
		else if (Input.GetKeyDown("d"))
		{
			nextHandhold = HandholdManager.Instance.NearestHandhold(Handhold.ButtonType.D, handPos, transform.position.y);
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
