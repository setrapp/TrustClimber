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

	public Handhold NearestHandhold(Handhold.ButtonType buttonType, Vector3 fromPosition, float aboveY)
	{
		GameObject[] handholds = GameObject.FindGameObjectsWithTag("Handhold");
		Handhold nearHandhold = null;
		float minSqrDist = 0;
		for (int i = 0; i < handholds.Length; i++)
		{
			Handhold handhold = handholds[i].GetComponent<Handhold>();
			float sqrDist = (handholds[i].transform.position - fromPosition).sqrMagnitude;
			if ((handhold != null && handhold.buttonType == buttonType && handhold.transform.position.y > aboveY) && (nearHandhold == null || sqrDist < minSqrDist))
			{
				nearHandhold = handhold;
				minSqrDist = sqrDist;
			}
		}

		return nearHandhold;
	}
}
