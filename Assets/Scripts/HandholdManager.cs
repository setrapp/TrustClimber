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

	private bool[] nearestValid = new bool[4];
	private Handhold[] nearest = new Handhold[4];
	GameObject[] handholds;

	void Awake()
	{
		handholds = GameObject.FindGameObjectsWithTag("Handhold");
	}

	void Update()
	{
		for (int i = 0; i < nearestValid.Length; i++)
		{
			nearestValid[i] = false;
		}
	}

	public Handhold NearestHandhold(Handhold.ButtonType buttonType)
	{
		if (nearestValid[(int)buttonType])
		{
			return nearest[(int)buttonType];
		}

		ClimbInput climber = ClimberManager.Instance.CurrentClimber;
		Handhold nearHandhold = null;
		float minSqrDist = climber.maxArmDistance;
		for (int i = 0; i < handholds.Length; i++)
		{
			Handhold handhold = handholds[i].GetComponent<Handhold>();
			float sqrDist = (handholds[i].transform.position - climber.transform.position).sqrMagnitude;
			bool correctDirection = (climber.moveUp && handhold.transform.position.y >= climber.handPos.y) || (!climber.moveUp && handhold.transform.position.y <= climber.handPos.y);
			if ((handhold != null && handhold.buttonType == buttonType && correctDirection) && (nearHandhold == null || sqrDist < minSqrDist))
			{
				nearHandhold = handhold;
				minSqrDist = sqrDist;
			}
		}

		nearest[(int)buttonType] = nearHandhold;
		nearestValid[(int)buttonType] = true;

		return nearHandhold;
	}
}
