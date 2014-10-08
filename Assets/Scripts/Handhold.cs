using UnityEngine;
using System.Collections;

public class Handhold : MonoBehaviour {
	public ButtonType buttonType;
	public TextMesh idText;

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
					renderer.material.color = Color.yellow;
				}
				else
					idText.text = "w";
				break;
			case ButtonType.Right:
				if(Input.GetJoystickNames().Length > 0)
				{
					idText.text = "B";
					renderer.material.color = Color.red;
				}
				else
					idText.text = "a";
				break;
			case ButtonType.Bottom:
				if(Input.GetJoystickNames().Length > 0)
				{
					idText.text = "A";
					renderer.material.color = Color.green;
				}
				else
					idText.text = "s";
				break;
			case ButtonType.Left:
				if(Input.GetJoystickNames().Length > 0)
				{
					idText.text = "X";
				renderer.material.color = Color.cyan;
				}
				else
					idText.text = "d";
				break;
		}
	}
}
