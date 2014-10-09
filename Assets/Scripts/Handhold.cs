using UnityEngine;
using System.Collections;

public class Handhold : MonoBehaviour {
	public ButtonType buttonType;
	public TextMesh idText;
	private ClimbInput climb;
	private GameObject player;

	//Rock falling Elements
	private GameObject leftHand;
	private GameObject rightHand;
	private Vector3 myPosition;
	public bool falling = false;
	private float fallRate = 4.0f;
	private float dislodge = 4.0f;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
		rightHand = player.GetComponent<ClimbInput>().rightHand;
		leftHand = player.GetComponent<ClimbInput>().leftHand;
		climb = player.GetComponent<ClimbInput>();
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
		switch(buttonType)
		{
			case ButtonType.Top:
				renderer.material.color = Color.yellow;
				if(Input.GetJoystickNames().Length > 0)
				{
					idText.text = "Y";
					if(HandholdManager.Instance.NearestHandhold(ButtonType.Top, climb.handPos, player.transform.position.y, climb.moveUp) == this)
						renderer.material.color = Color.yellow;
					else
						renderer.material.color = Color.white;
				}
				else
				{
					idText.text = "w";
					if(HandholdManager.Instance.NearestHandhold(ButtonType.Top, climb.handPos, player.transform.position.y, climb.moveUp) == this)
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
					if(HandholdManager.Instance.NearestHandhold(ButtonType.Right, climb.handPos, player.transform.position.y, climb.moveUp) == this)
						renderer.material.color = Color.red;
					else
						renderer.material.color = Color.white;
				}
				else
				{
					idText.text = "d";
					if(HandholdManager.Instance.NearestHandhold(ButtonType.Right, climb.handPos, player.transform.position.y, climb.moveUp) == this)
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
					if(HandholdManager.Instance.NearestHandhold(ButtonType.Bottom, climb.handPos, player.transform.position.y, climb.moveUp) == this)
						renderer.material.color = Color.green;
					else
						renderer.material.color = Color.white;
				}
				else
				{
					idText.text = "s";
					if(HandholdManager.Instance.NearestHandhold(ButtonType.Bottom, climb.handPos, player.transform.position.y, climb.moveUp) == this)
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
					if(HandholdManager.Instance.NearestHandhold(ButtonType.Left, climb.handPos, player.transform.position.y, climb.moveUp) == this)
						renderer.material.color = Color.blue;
					else
						renderer.material.color = Color.white;
				}
				else
				{
					idText.text = "a";
					if(HandholdManager.Instance.NearestHandhold(ButtonType.Left, climb.handPos, player.transform.position.y, climb.moveUp) == this)
						renderer.material.color = Color.blue;
					else
						renderer.material.color = Color.white;
				}
				break;
		}

		if(leftHand.transform.position == myPosition || rightHand.transform.position == myPosition)
		{
			//print ("held");
			Invoke("AboutToFall",dislodge);
		}

		if(falling)
		{
			if(leftHand.transform.position == myPosition)
			{
				player.SendMessage("SlipLeft",SendMessageOptions.DontRequireReceiver);
			}
			else if (rightHand.transform.position == myPosition)
			{
				player.SendMessage("SlipRight",SendMessageOptions.DontRequireReceiver);
			}
			transform.Translate(Vector3.down * Time.deltaTime * fallRate);
		}

	}

	public void AboutToFall()
	{
		falling = true;
	}
}