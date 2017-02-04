using UnityEngine;
using System.Collections;

// spherical hero controls!
// -use raycast to determine where sphero is standing,
// -reorient sphero based on collision poly surface normal
public class Sphero : MonoBehaviour {

	// using raycast
	public bool bdebug;		// visualize my raycasts
	public Vector3 vStart;
	public Vector3 vDown;  	// our down vector
	public float fLength;	// length of the down vector to test against
	public float heightOffset;
	public float kh;

	public Color colColor;	// raycast result color
	public Color hitColor;	// ray intersected something
	public Color missColor;	// ray intersected nothing

	public float walkSpeed;
	public float turnSpeed;
	public float localYRot;

	private CapsuleCollider cc;

	// Use this for initialization
	void Start () {
	
		cc = this.gameObject.GetComponent<CapsuleCollider> ();
	}

	public void SpheroUpdate()
	{
		if (cc != null)
			heightOffset = cc.height;

		// update vStart
		vStart = this.transform.position;
		vDown = -this.transform.up;

		colColor = missColor;
		RaycastHit hitInfo;
		bool bIntersection = Physics.Raycast (vStart, vDown, out hitInfo, fLength);
		if (bIntersection == true)
		{
			colColor = hitColor;	

			// if the raycast was a hit... glue the sphero to the hitpoint
			this.transform.position = hitInfo.point + 0.5f*heightOffset*-vDown;
			//this.transform.up = hitInfo.normal; //clobber is so brute force, lets be elegant.
			this.transform.up += kh * (hitInfo.normal - this.transform.up);
		}

		if (bdebug == true) {
			// draw our raycast
			Debug.DrawLine (vStart, vStart + fLength * vDown, colColor);
		}

		if (Input.GetKey (KeyCode.LeftArrow) == true)
			localYRot += (-turnSpeed * Time.deltaTime);
			//this.gameObject.transform.Rotate( Vector3.up, -turnSpeed * Time.deltaTime, Space.World);
		if (Input.GetKey (KeyCode.RightArrow) == true)
			localYRot += (turnSpeed * Time.deltaTime);
			//this.gameObject.transform.Rotate ( Vector3.up, turnSpeed * Time.deltaTime, Space.World);

		this.transform.Rotate (Vector3.up, localYRot, Space.Self);

		//Vector3 vRightNew = Vector3.Cross (Vector3.up + new Vector3(0.0f,0.01f,0.0f), this.transform.up);
		//Vector3 vFwdNew = Vector3.Cross (this.transform.right, this.transform.up);

		if (Input.GetKey (KeyCode.UpArrow) == true)
			this.gameObject.transform.Translate (walkSpeed * Time.deltaTime * /*vFwdNew*/this.transform.forward, Space.World);

	}

	// Update is called once per frame
	void Update () {
	
		SpheroUpdate ();

	}
}
