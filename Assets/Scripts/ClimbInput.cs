using UnityEngine;
using System.Collections;

public class ClimbInput : MonoBehaviour {
	public bool isClimbing = false;
	public ClimbInput partner;
	public GameObject leftHand;
	public GameObject rightHand;
	private bool movingLeftHand = true;
	public float bodyBelowHands;
	[HideInInspector]
	public float maxArmDistance;
	public float taughtRopeReach;
	public float slackRopeReach;
	public Vector3 handPos;
	public Color normalHandColor;
	public Color highlightHandColor;
	public float maxPartnerDistance;
	public bool moveUp;

	//Falling
	public Vector3 leftHang;
	public Vector3 rightHang;
	public bool lHandHanging = false;
	public bool rHandHanging = false;
	private float fallRate = 0.0f;
	public bool canPullUp;

	// Endstate 
	public GameObject platform = null;
	public bool onPlatform = false;
	private Vector3 platformStand;
	public bool safe = false;
	public Vector3 leftVict;
	public Vector3 rightVict;

	void Awake()
	{
		MoveBodyBetweenHands();
		ResetHandColors();
	}

	void Start()
	{
		leftHang = new Vector3(-0.75f,-0.7f,0.0f);
		rightHang = new Vector3(0.75f,-0.7f,0.0f);
		leftVict = new Vector3(-0.75f,0.9f,0.0f);
		rightVict = new Vector3(0.75f,0.9f,0.0f);
		if (platform != null)
		{
			platformStand.y = platform.transform.position.y;
			platformStand.y += 1.5f;
		}
	}
	void Update()
	{
		if (!ClimberManager.Instance.rope.IsTaught)
		{
			
			maxArmDistance = slackRopeReach;
			
		}
		else
		{
			maxArmDistance = taughtRopeReach;
		}

		if (Input.GetButtonDown("Switch Climber"))
		{
			isClimbing = !isClimbing;
			ResetHandColors();
			
			/*if (partner != null)
			{
				partner.isClimbing = true;
			}*/
		}

		if (isClimbing)
		{
			moveUp = true;
			if (Input.GetAxis("Look Down") != 0)
			{
				moveUp = false;
			}

			Handhold nextHandhold = null;
			handPos = leftHand.transform.position;
			if (!movingLeftHand)
			{
				handPos = rightHand.transform.position;
			}

			if (Input.GetButtonDown("Top"))
			{
				nextHandhold = HandholdManager.Instance.NearestHandhold(Handhold.ButtonType.Top);
			}
			else if (Input.GetButtonDown("Left"))
			{
				nextHandhold = HandholdManager.Instance.NearestHandhold(Handhold.ButtonType.Left);
			}
			else if (Input.GetButtonDown("Bottom"))
			{
				nextHandhold = HandholdManager.Instance.NearestHandhold(Handhold.ButtonType.Bottom);
			}
			else if (Input.GetButtonDown("Right"))
			{
				nextHandhold = HandholdManager.Instance.NearestHandhold(Handhold.ButtonType.Right);
			}

			if (nextHandhold != null && (nextHandhold.transform.position - transform.position).sqrMagnitude <= Mathf.Pow(maxArmDistance, 2))
			{
				if (movingLeftHand)
				{
					leftHand.transform.position = nextHandhold.transform.position;
					leftHand.renderer.material.color = normalHandColor;
					rightHand.renderer.material.color = highlightHandColor;
					lHandHanging = false;
				}
				else
				{
					rightHand.transform.position = nextHandhold.transform.position;
					rightHand.renderer.material.color = normalHandColor;
					leftHand.renderer.material.color = highlightHandColor;
					rHandHanging = false;
				}

				movingLeftHand = !movingLeftHand;
				MoveBodyBetweenHands();
			}
		}

		transform.Translate(Vector3.down * Time.deltaTime * fallRate);
		platformStand.x = transform.position.x;
		platformStand.z = transform.position.z;

		if(lHandHanging && rHandHanging)
		{
			fallRate = 4.0f;
			//print("falling");
			MoveBodyBetweenHands();
		}

		if(onPlatform == true)
		{
			//print("On Platform");
			fallRate = 0.0f;
			lHandHanging = false;
			rHandHanging = false;
			transform.position = platformStand;
			leftHand.transform.localPosition = leftHang;
			rightHand.transform.localPosition = rightHang;
			onPlatform = false;
		}

		if(platform != null && transform.position.y > platform.transform.position.y + 2.0f)
		{
			//platform.collider.enabled = true;
		}
	}// End of Update

	private void ResetHandColors()
	{
		leftHand.renderer.material.color = normalHandColor;
		rightHand.renderer.material.color = normalHandColor;
		if (isClimbing)
		{
			if (movingLeftHand)
			{
				leftHand.renderer.material.color = highlightHandColor;
			}
			else
			{
				rightHand.renderer.material.color = normalHandColor;
			}
		}
	}

	private void MoveBodyBetweenHands()
	{
		Vector3 newPosition = (leftHand.transform.position + rightHand.transform.position) / 2;
		newPosition.y -= bodyBelowHands;
		if (partner != null && (newPosition - partner.transform.position).sqrMagnitude > Mathf.Pow(maxPartnerDistance, 2))
		{
			if (!ClimberManager.Instance.rope.ropeLost)
			{
				if (Input.GetAxis("Braking") > .5 || !(lHandHanging && rHandHanging))
				{
					Vector3 fromPartner = (newPosition - partner.transform.position).normalized;
					newPosition = partner.transform.position + (fromPartner * maxPartnerDistance);
				}
				else
				{
					if (!ClimberManager.Instance.rope.RopeOut())
						ClimberManager.Instance.rope.LoseRope();
				}
			}
		}
		leftHand.transform.position -= (newPosition - transform.position);
		rightHand.transform.position -= (newPosition - transform.position);
		if (lHandHanging && leftHand.transform.position.y < transform.position.y && (leftHand.transform.position - transform.position).sqrMagnitude >= Mathf.Pow(maxArmDistance / 4, 2))
		{
			Vector3 toHand = (leftHand.transform.position - transform.position).normalized;
			leftHand.transform.position = transform.position + (toHand * maxArmDistance / 4);
		}
		if (rHandHanging && rightHand.transform.position.y < transform.position.y && (rightHand.transform.position - transform.position).sqrMagnitude >= Mathf.Pow(maxArmDistance / 4, 2))
		{
			Vector3 toHand = (rightHand.transform.position - transform.position).normalized;
			rightHand.transform.position = transform.position + (toHand * maxArmDistance / 4);
		}
		transform.position = newPosition;
	}

	public void SlipLeft()
	{
		leftHand.transform.localPosition = leftHang;
		lHandHanging = true;
	}

	public void SlipRight()
	{
		rightHand.transform.localPosition = rightHang;
		rHandHanging = true;
	}

	void OnTriggerEnter(Collider collide)
	{
		if(collide.gameObject == platform)
		{
			onPlatform = true;
			//platform.collider.enabled = false;
			safe = true;
		}
	}
	void OnTriggerExit(Collider collide)
	{
		if(collide.gameObject == platform)
		{
			safe = false;
		}
	}
	void VictHands()
	{
		leftHand.transform.localPosition = leftVict;
		rightHand.transform.localPosition = rightVict;
		print ("victory");
	}
	
}
