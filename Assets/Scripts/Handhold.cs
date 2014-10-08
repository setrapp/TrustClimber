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
				renderer.material.color = Color.yellow;
				if(Input.GetJoystickNames().Length > 0)
				{
					idText.text = "Y";
					
				}
				else
					idText.text = "w";
				break;
			case ButtonType.Right:
				renderer.material.color = Color.red;
				if(Input.GetJoystickNames().Length > 0)
				{
					idText.text = "B";
					
				}
				else
					idText.text = "a";
				break;
			case ButtonType.Bottom:
				renderer.material.color = Color.green;
				if(Input.GetJoystickNames().Length > 0)
				{
					idText.text = "A";
				}
				else
					idText.text = "s";
				break;
			case ButtonType.Left:
				renderer.material.color = Color.cyan;
				if(Input.GetJoystickNames().Length > 0)
				{
					idText.text = "X";
				}
				else
					idText.text = "d";
				break;
		}
	}
}
