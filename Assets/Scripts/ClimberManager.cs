using UnityEngine;
using System.Collections;

public class ClimberManager : MonoBehaviour {
	public ClimbInput climber1;
	public ClimbInput climber2;

	void Update()
	{
		Debug.DrawLine(climber1.transform.position, climber2.transform.position, Color.black);
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		Gizmos.DrawLine(climber1.transform.position, climber2.transform.position);
	}
}
