using UnityEngine;
using System.Collections;

public class Handhold : MonoBehaviour {
	public ButtonType buttonType;
	public TextMesh idText;

	void Start(){
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

		switch(buttonType)
		{
			case ButtonType.Top:
				renderer.material.color = Color.yellow;
				if(Input.GetJoystickNames().Length > 0)
				{
					idText.text = "Y";
					if (HandholdManager.Instance.NearestHandhold(ButtonType.Top, climber.handPos, climber.transform.position.y, climber.moveUp) == this)
						renderer.material.color = Color.yellow;
					else
						renderer.material.color = Color.white;
				}
				else
				{
					idText.text = "w";
					if (HandholdManager.Instance.NearestHandhold(ButtonType.Top, climber.handPos, climber.transform.position.y, climber.moveUp) == this)
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
					if (HandholdManager.Instance.NearestHandhold(ButtonType.Right, climber.handPos, climber.transform.position.y, climber.moveUp) == this)
						renderer.material.color = Color.red;
					else
						renderer.material.color = Color.white;
				}
				else
				{
					idText.text = "d";
					if (HandholdManager.Instance.NearestHandhold(ButtonType.Right, climber.handPos, climber.transform.position.y, climber.moveUp) == this)
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
					if (HandholdManager.Instance.NearestHandhold(ButtonType.Bottom, climber.handPos, climber.transform.position.y, climber.moveUp) == this)
						renderer.material.color = Color.green;
					else
						renderer.material.color = Color.white;
				}
				else
				{
					idText.text = "s";
					if (HandholdManager.Instance.NearestHandhold(ButtonType.Bottom, climber.handPos, climber.transform.position.y, climber.moveUp) == this)
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
					if (HandholdManager.Instance.NearestHandhold(ButtonType.Left, climber.handPos, climber.transform.position.y, climber.moveUp) == this)
						renderer.material.color = Color.blue;
					else
						renderer.material.color = Color.white;
				}
				else
				{
					idText.text = "a";
					if (HandholdManager.Instance.NearestHandhold(ButtonType.Left, climber.handPos, climber.transform.position.y, climber.moveUp) == this)
						renderer.material.color = Color.blue;
					else
						renderer.material.color = Color.white;
				}
				break;
		}
	}
}
