using UnityEngine;
using System.Collections;

public class ClimbInput : MonoBehaviour {
	public bool isClimbing = false;
	public ClimbInput partner;
	public GameObject leftHand;
	public GameObject rightHand;
	private bool movingLeftHand = true;
	public float bodyBelowHands;
	public float maxArmDistance;
	public Color normalHandColor;
	public Color highlightHandColor;

	void Awake()
	{
		MoveBodyBetweenHands();
		leftHand.renderer.material.color = highlightHandColor;
		rightHand.renderer.material.color = normalHandColor;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			isClimbing = !isClimbing;
			/*if (partner != null)
			{
				partner.isClimbing = true;
			}*/
		}

		if (isClimbing)
		{
			bool moveUp = true;
			if (Input.GetKey(KeyCode.LeftShift))
			{
				moveUp = false;
			}

			Handhold nextHandhold = null;
			Vector3 handPos = leftHand.transform.position;
			if (!movingLeftHand)
			{
				handPos = rightHand.transform.position;
			}

			if (Input.GetKeyDown("w"))
			{
				nextHandhold = HandholdManager.Instance.NearestHandhold(Handhold.ButtonType.W, handPos, transform.position.y, moveUp);
			}
			else if (Input.GetKeyDown("a"))
			{
				nextHandhold = HandholdManager.Instance.NearestHandhold(Handhold.ButtonType.A, handPos, transform.position.y, moveUp);
			}
			else if (Input.GetKeyDown("s"))
			{
				nextHandhold = HandholdManager.Instance.NearestHandhold(Handhold.ButtonType.S, handPos, transform.position.y, moveUp);
			}
			else if (Input.GetKeyDown("d"))
			{
				nextHandhold = HandholdManager.Instance.NearestHandhold(Handhold.ButtonType.D, handPos, transform.position.y, moveUp);
			}

			if (nextHandhold != null && (nextHandhold.transform.position - transform.position).sqrMagnitude <= Mathf.Pow(maxArmDistance, 2))
			{
				if (movingLeftHand)
				{
					leftHand.transform.position = nextHandhold.transform.position;
					leftHand.renderer.material.color = normalHandColor;
					rightHand.renderer.material.color = highlightHandColor;
				}
				else
				{
					rightHand.transform.position = nextHandhold.transform.position;
					rightHand.renderer.material.color = normalHandColor;
					leftHand.renderer.material.color = highlightHandColor;
				}

				movingLeftHand = !movingLeftHand;
				MoveBodyBetweenHands();
			}
		}
		else
		{
			if (leftHand.renderer.material.color != normalHandColor || rightHand.renderer.material.color != normalHandColor)
			{
				leftHand.renderer.material.color = normalHandColor;
				rightHand.renderer.material.color = normalHandColor;
			}
		}
	}

	private void MoveBodyBetweenHands()
	{
		Vector3 newPosition = (leftHand.transform.position + rightHand.transform.position) / 2;
		newPosition.y -= bodyBelowHands;
		leftHand.transform.position -= (newPosition - transform.position);
		rightHand.transform.position -= (newPosition - transform.position);
		transform.position = newPosition;
	}
}
