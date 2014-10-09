using UnityEngine;
using System.Collections;

public class Handhold : MonoBehaviour {
	public ButtonType buttonType;
	public TextMesh idText;
	private ClimbInput climb;
	private GameObject player;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
		climb = player.GetComponent<ClimbInput>();
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
				if(Input.GetJoystickNames().Length > 0)
				{
					idText.text = "Y";
					if(HandholdManager.Instance.NearestHandhold(ButtonType.Top, climb.handPos, player.transform.position.y) == this)
						renderer.material.color = Color.yellow;
					else
						renderer.material.color = Color.white;
				}
				else
				{
					idText.text = "w";
					if(HandholdManager.Instance.NearestHandhold(ButtonType.Top, climb.handPos, player.transform.position.y) == this)
						renderer.material.color = Color.yellow;
					else
						renderer.material.color = Color.white;
				}
				break;
			case ButtonType.Right:
				if(Input.GetJoystickNames().Length > 0)
				{
					idText.text = "B";
					if(HandholdManager.Instance.NearestHandhold(ButtonType.Right, climb.handPos, player.transform.position.y) == this)
						renderer.material.color = Color.red;
					else
						renderer.material.color = Color.white;
				}
				else
				{
					idText.text = "d";
					if(HandholdManager.Instance.NearestHandhold(ButtonType.Right, climb.handPos, player.transform.position.y) == this)
						renderer.material.color = Color.red;
					else
						renderer.material.color = Color.white;
				}
				break;
			case ButtonType.Bottom:
				if(Input.GetJoystickNames().Length > 0)
				{
					idText.text = "A";
					if(HandholdManager.Instance.NearestHandhold(ButtonType.Bottom, climb.handPos, player.transform.position.y) == this)
						renderer.material.color = Color.green;
					else
						renderer.material.color = Color.white;
				}
				else
				{
					idText.text = "s";
					if(HandholdManager.Instance.NearestHandhold(ButtonType.Bottom, climb.handPos, player.transform.position.y) == this)
						renderer.material.color = Color.green;
					else
						renderer.material.color = Color.white;
				}
				break;
			case ButtonType.Left:
				if(Input.GetJoystickNames().Length > 0)
				{
					idText.text = "X";
					if(HandholdManager.Instance.NearestHandhold(ButtonType.Left, climb.handPos, player.transform.position.y) == this)
						renderer.material.color = Color.blue;
					else
						renderer.material.color = Color.white;
				}
				else
				{
					idText.text = "a";
					if(HandholdManager.Instance.NearestHandhold(ButtonType.Left, climb.handPos, player.transform.position.y) == this)
						renderer.material.color = Color.blue;
					else
						renderer.material.color = Color.white;
				}
				break;
		}
	}
}
