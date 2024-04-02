using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
	static public GameObject POI; // the static POI
	[Header("Inscribed")]
	public float 		easing = 0.05f;
	public Vector2		minXY  = Vector2.zero; // Vector2.zero is [0,0]
	
	[Header("Dynamic")]
	public float camZ; // the desired Z pos of the camera
	
	void Awake(){
		camZ = this.transform.position.z;
	}
	
	void FixedUpdate () {
		
		Vector3 destination = Vector3.zero;
		
		if( POI != null ) {
			// if POI has a Rigidbody, check to see if it is sleeping
			Rigidbody poiRigid = POI.GetComponent<Rigidbody>();
			if ( ( poiRigid != null ) && poiRigid.IsSleeping() ) {
				POI = null;
			}
		}
		
		if ( POI != null ){
			destination = POI.transform.position;
		}
		
		//Limit the min values of destination x and y
		destination.x = Mathf.Max( minXY.x, destination.x );
		destination.y = Mathf.Max( minXY.y, destination.y );
		//Interpolate from the current Camera position towards destination
		destination = Vector3.Lerp(transform.position, destination, easing);
		// Force destination.z to be camZ to keep the camera far enough away
		destination.z = camZ;
		//Set cemera to the destination
		transform.position = destination;
		//Set the orthographicSize of the Camera to keep the ground in view 
		Camera.main.orthographicSize = destination.y + 10;
	}
}
