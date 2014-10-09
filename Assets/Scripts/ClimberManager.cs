using UnityEngine;
using System.Collections;

public class ClimberManager : MonoBehaviour {
	private static ClimberManager instance = null;
	public static ClimberManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindGameObjectWithTag("Globals").GetComponent<ClimberManager>();
			}
			return instance;
		}
	}
	public ClimbInput climber1;
	public ClimbInput climber2;
	public ClimbInput CurrentClimber
	{
		get
		{
			if (climber1.isClimbing)
			{
				return climber1;
			}
			return climber2;
		}
	}
	public ClimbInput CurrentBelayer
	{
		get
		{
			if (!climber1.isClimbing)
			{
				return climber1;
			}
			return climber2;
		}
	}

	public TestRopeScriptFromWeb rope;

	void Update()
	{
		Vector3 climb1ViewportPos = Camera.main.WorldToViewportPoint(climber1.transform.position);
		Vector3 climb2ViewportPos = Camera.main.WorldToViewportPoint(climber2.transform.position);
		if (climb1ViewportPos.x < 0 || climb1ViewportPos.x > 1 || climb1ViewportPos.y < 0 || climb1ViewportPos.y > 1 || 
			climb2ViewportPos.x < 0 || climb2ViewportPos.x > 1 || climb2ViewportPos.y < 0 || climb2ViewportPos.y > 1)
		{
			Application.LoadLevel(Application.loadedLevelName);
		}
	}

	void OnDrawGizmos()
	{
	/*	if (climber1 != null && climber2 != null)
		{
			Gizmos.color = Color.black;
			Gizmos.DrawLine(climber1.transform.position, climber2.transform.position);
		}
	*/
	}
}
