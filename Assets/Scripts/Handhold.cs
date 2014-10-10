using UnityEngine;
using System.Collections;

public class Handhold : MonoBehaviour {
	public ButtonType buttonType;
	public TextMesh idText;

	//Rock falling Elements
	private GameObject cleftHand;
	private GameObject crightHand;
	private GameObject bleftHand;
	private GameObject brightHand;
	private Vector3 myPosition;
	public bool stable = true;
	public bool falling = false;
	private float fallRate = 4.0f;
	private float dislodge = 4.0f;
	public bool isHeld = false;

	void Start(){
		myPosition = transform.position;
	}

	public enum ButtonType
	{
		Top = 0,
		Left,
		Bottom,
		Right,
	}

	void Update()
	{
		ClimbInput climber = ClimberManager.Instance.CurrentClimber;
		ClimbInput belayer = ClimberManager.Instance.CurrentBelayer;
		crightHand = climber.rightHand;
		cleftHand = climber.leftHand;
		brightHand = belayer.rightHand;
		bleftHand = belayer.leftHand;

		switch(buttonType)
		{
			case ButtonType.Top:
				renderer.material.color = Color.yellow;
				if(Input.GetJoystickNames().Length > 0)
				{
					idText.text = "Y";
					if (HandholdManager.Instance.NearestHandhold(ButtonType.Top) == this)
						renderer.material.color = Color.yellow;
					else
						renderer.material.color = Color.white;
				}
				else
				{
					idText.text = "W";
					if (HandholdManager.Instance.NearestHandhold(ButtonType.Top) == this && (transform.position - climber.transform.position).sqrMagnitude <= Mathf.Pow(climber.maxArmDistance, 2))
						renderer.material.color = Color.yellow;
					else
						renderer.material.color = Color.white;
				}
				break;
			case ButtonType.Right:
				renderer.material.color = Color.red;
				if(Input.GetJoystickNames().Length > 0)
				{
					idText.text = "B";
					if (HandholdManager.Instance.NearestHandhold(ButtonType.Right) == this && (transform.position - climber.transform.position).sqrMagnitude <= Mathf.Pow(climber.maxArmDistance, 2))
						renderer.material.color = Color.red;
					else
						renderer.material.color = Color.white;
				}
				else
				{
					idText.text = "D";
					if (HandholdManager.Instance.NearestHandhold(ButtonType.Right) == this && (transform.position - climber.transform.position).sqrMagnitude <= Mathf.Pow(climber.maxArmDistance, 2))
						renderer.material.color = Color.red;
					else
						renderer.material.color = Color.white;
				}
				break;
			case ButtonType.Bottom:
				renderer.material.color = Color.green;
				if(Input.GetJoystickNames().Length > 0)
				{
					idText.text = "A";
					if (HandholdManager.Instance.NearestHandhold(ButtonType.Bottom) == this && (transform.position - climber.transform.position).sqrMagnitude <= Mathf.Pow(climber.maxArmDistance, 2))
						renderer.material.color = Color.green;
					else
						renderer.material.color = Color.white;
				}
				else
				{
					idText.text = "S";
					if (HandholdManager.Instance.NearestHandhold(ButtonType.Bottom) == this && (transform.position - climber.transform.position).sqrMagnitude <= Mathf.Pow(climber.maxArmDistance, 2))
						renderer.material.color = Color.green;
					else
						renderer.material.color = Color.white;
				}
				break;
			case ButtonType.Left:
				renderer.material.color = Color.cyan;
				if(Input.GetJoystickNames().Length > 0)
				{
					idText.text = "X";
					if (HandholdManager.Instance.NearestHandhold(ButtonType.Left) == this && (transform.position - climber.transform.position).sqrMagnitude <= Mathf.Pow(climber.maxArmDistance, 2))
						renderer.material.color = Color.blue;
					else
						renderer.material.color = Color.white;
				}
				else
				{
					idText.text = "A";
					if (HandholdManager.Instance.NearestHandhold(ButtonType.Left) == this && (transform.position - climber.transform.position).sqrMagnitude <= Mathf.Pow(climber.maxArmDistance, 2))
					{
						renderer.material.color = Color.blue;
						idText.color = Color.white;
					}
					else
						renderer.material.color = Color.white;
				}
				break;
		}

		if(!stable && (cleftHand.transform.position == myPosition || crightHand.transform.position == myPosition))
		{
			//print ("held");		
			Invoke("AboutToFall",dislodge);
		}

		if(bleftHand.transform.position == myPosition || brightHand.transform.position == myPosition)
		{
			isHeld = true;
			//print("held");
		}
		else
			isHeld = false;

		if(falling)
		{
			if(cleftHand.transform.position == myPosition)
			{
				ClimberManager.Instance.CurrentClimber.SendMessage("SlipLeft",SendMessageOptions.DontRequireReceiver);
			}
			else if (crightHand.transform.position == myPosition)
			{
				ClimberManager.Instance.CurrentClimber.SendMessage("SlipRight",SendMessageOptions.DontRequireReceiver);
			}
			transform.Translate(Vector3.down * Time.deltaTime * fallRate);
		}

	}// End of Update

	public void AboutToFall()
	{
		falling = true;
	}
}