using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform climber1;
    public Transform climber2;
    public float smoothness;
    public Vector3 targetPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        targetPosition = new Vector3((climber1.position.x + climber2.position.x) / 2, (climber1.position.y + climber2.position.y) / 2, -10);
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.5f);
	}
}
