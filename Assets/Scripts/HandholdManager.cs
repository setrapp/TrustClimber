using UnityEngine;
using System.Collections;

public class HandholdManager : MonoBehaviour {
	private static HandholdManager instance = null;
	public static HandholdManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindGameObjectWithTag("Globals").GetComponent<HandholdManager>();
			}
			return instance;
		}
	}

	public Handhold NearestHandhold(Handhold.ButtonType buttonType, Vector3 fromPosition, float extremeY, bool moveUp)
	{
		GameObject[] handholds = GameObject.FindGameObjectsWithTag("Handhold");
		Handhold nearHandhold = null;
		float minSqrDist = 0;
		for (int i = 0; i < handholds.Length; i++)
		{
			Handhold handhold = handholds[i].GetComponent<Handhold>();
			float sqrDist = (handholds[i].transform.position - fromPosition).sqrMagnitude;
			bool correctDirection = (moveUp && handhold.transform.position.y >= extremeY) || (!moveUp && handhold.transform.position.y <= extremeY);
			if ((handhold != null && handhold.buttonType == buttonType && correctDirection) && (nearHandhold == null || sqrDist < minSqrDist) && (handhold.isHeld != true))
			{
				nearHandhold = handhold;
				minSqrDist = sqrDist;
			}
		}

		return nearHandhold;
	}
}
        