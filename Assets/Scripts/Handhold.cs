using UnityEngine;
using System.Collections;

public class Handhold : MonoBehaviour {
	public ButtonType buttonType;
	public TextMesh idText;

	public enum ButtonType
	{
		W = 0,
		A,
		S,
		D,
	}

	void Update()
	{
		switch(buttonType)
		{
			case ButtonType.W:
				idText.text = "w";
				break;
			case ButtonType.A:
				idText.text = "a";
				break;
			case ButtonType.S:
				idText.text = "s";
				break;
			case ButtonType.D:
				idText.text = "d";
				break;
		}
	}
}
