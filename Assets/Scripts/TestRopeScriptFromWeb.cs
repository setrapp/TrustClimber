using UnityEngine;
using System.Collections;

// Require a Rigidbody and LineRenderer object for easier assembly
[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (LineRenderer))]

public class TestRopeScriptFromWeb : MonoBehaviour {
	/*========================================
	==  Physics Based Rope				==
	==  File: Rope.js					  ==
	==  Original by: Jacob Fletcher		==
	==  Use and alter Freely			 ==
	==  CSharp Conversion by: Chelsea Hash
	==  Alterations by: Ben Snyder
	==========================================
	How To Use:
	 ( BASIC )
	 1. Simply add this script to the object you want a rope teathered to
	 2. In the "LineRenderer" that is added, assign a material and adjust the width settings to your likeing
	 3. Assign the other end of the rope as the "Target" object in this script
	 4. Play and enjoy!
 
	 ( Advanced )
	 1. Do as instructed above
	 2. If you want more control over the rigidbody on the ropes end go ahead and manually
		 add the rigidbody component to the target end of the rope and adjust acordingly.
	 3. adjust settings as necessary in both the rigidbody and rope script
 
	 (About Character Joints)
	 Sometimes your rope needs to be very limp and by that I mean NO SPRINGY EFFECT.
	 In order to do this, you must loosen it up using the swingAxis and twist limits.
	 For example, On my joints in my drawing app, I set the swingAxis to (0,0,1) sense
	 the only axis I want to swing is the Z axis (facing the camera) and the other settings to around -100 or 100.
 
 
	*/

	public GameObject test;
	public Transform target;
	public float resolution = 2F;							  //  Sets the amount of joints there are in the rope (1 = 1 joint for every 1 unit)
	public float ropeDrag = 0.1F;								 //  Sets each joints Drag
	public float ropeMass = 1F;							//  Sets each joints Mass

	public float sjDistance = 1f;
	public float sjDamping = 2f;
	public float sjFrequency = 0f;

	private Vector3[] segmentPos;			//  DONT MESS!	This is for the Line Renderer's Reference and to set up the positions of the gameObjects
	private GameObject[] joints;			//  DONT MESS!	This is the actual joint objects that will be automatically created
	private LineRenderer line;							//  DONT MESS!	 The line renderer variable is set up when its assigned as a new component
	private int segments = 0;					//  DONT MESS!	The number of segments is calculated based off of your distance * resolution
	private bool rope = false;						 //  DONT MESS!	This is to keep errors out of your debug window! Keeps the rope from rendering when it doesnt exist...


	private int segmentNumber = 0;
	
	void Awake()
	{
		BuildRope();
	}
	
	void Update()
	{
		if(Input.GetButtonDown("RopeIn"))
		{
			if(segmentNumber == 0)
			{
				test.GetComponent<HingeJoint2D>().connectedBody = joints[1].rigidbody2D;
				segmentNumber ++;
			}
			else
			{
				if(segmentNumber < segments -2)
				{
					segmentNumber++;
					Debug.Log(segmentNumber);
					test.GetComponent<HingeJoint2D>().connectedBody = joints[segmentNumber].rigidbody2D;
				}
			}
		}

		if(Input.GetButtonDown("RopeOut"))
		{
			if(segmentNumber > 0)
			{
				segmentNumber--;
				if(segmentNumber == 0)
					test.GetComponent<HingeJoint2D>().connectedBody = gameObject.rigidbody2D;
				else
					test.GetComponent<HingeJoint2D>().connectedBody = joints[segmentNumber].rigidbody2D;
			}
		}

	}

	void LateUpdate()
	{
		// Does rope exist? If so, update its position
		if(rope) {
			for(int i=0;i<segments;i++) {
				if(i == 0) {
					line.SetPosition(i,transform.position);
				} else
				if(i == segments-1) {
					line.SetPosition(i,target.transform.position);	
				} else {
					line.SetPosition(i,joints[i].transform.position);
				}
			}
			line.enabled = true;
		} else {
			line.enabled = false;	
		}
	}
	
	
	
	void BuildRope()
	{
		line = gameObject.GetComponent<LineRenderer>();
		
		// Find the amount of segments based on the distance and resolution
		// Example: [resolution of 1.0 = 1 joint per unit of distance]
		segments = (int)(Vector3.Distance(transform.position,target.position)*resolution);
		line.SetVertexCount(segments);
		segmentPos = new Vector3[segments];
		joints = new GameObject[segments];
		Debug.Log(joints.Length);
		segmentPos[0] = transform.position;
		segmentPos[segments-1] = target.position;
		
		// Find the distance between each segment
		var segs = segments-1;
		var seperation = ((target.position - transform.position)/segs);
		
		for(int s=1;s < segments;s++)
		{
			// Find the each segments position using the slope from above
			Vector3 vector = (seperation*s) + transform.position;	
			segmentPos[s] = vector;
			
			//Add Physics to the segments
			AddJointPhysics(s);
		}

		SpringJoint2D end = target.gameObject.AddComponent<SpringJoint2D>();
		end.connectedBody = joints[joints.Length-1].transform.rigidbody2D;
		end.distance = sjDistance;
		end.dampingRatio = sjDamping;
		end.frequency = sjFrequency;
		
		// Rope = true, The rope now exists in the scene!
		rope = true;
	}
	
	void AddJointPhysics(int n)
	{
		joints[n] = new GameObject("Joint_" + n);
		joints[n].transform.parent = transform;
		Rigidbody2D rigid = joints[n].AddComponent<Rigidbody2D>();

		SpringJoint2D sj = joints[n].AddComponent<SpringJoint2D>();

		sj.collideConnected = false;
		sj.distance = sjDistance;
		sj.dampingRatio =sjDamping;
		sj.frequency = sjFrequency;
		
		joints[n].transform.position = segmentPos[n];

		rigid.drag = ropeDrag;
		rigid.mass = ropeMass;
		
		if(n==1){		
			sj.connectedBody = transform.rigidbody2D;
		} else
		{
			sj.connectedBody = joints[n-1].rigidbody2D;
		}
		
	}
	
	void DestroyRope()
	{
		// Stop Rendering Rope then Destroy all of its components
		rope = false;
		for(int dj=0;dj<joints.Length-1;dj++)
		{
			Destroy(joints[dj]);	
		}
		Destroy(target.gameObject.GetComponent<SpringJoint2D>());
		segmentPos = new Vector3[0];
		joints = new GameObject[0];
		segments = 0;
	}
}