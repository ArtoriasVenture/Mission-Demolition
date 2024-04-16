using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
	static public Hero 	S { get; private set; } //Singleton property 
	
	[Header("Inscribed")]
	//These fields control movement
	public float			speed = 30;
	public float			rollMult = -45;
	public float			pitchMult = 30;
	
	[Header("Dynamic")] [Range(0,4)]
    public float			shieldLevel = 1;
	
	void awake() {
		if (S == null) {
			S = this; // set singleton only if it's null
		}else {
			Debug.LogError("Hero.Awake() - Attempted to assign secpnd Hero.S!");
		}
	}
	
    // Update is called once per frame
    void Update()
    {
	//pull info from input class
	float hAxis = Input.GetAxis("Horizontal");
	float vAxis = Input.GetAxis("Vertical");
	
	//change transform.position based on the axes
	Vector3 pos = transform.position;
	pos.x += hAxis * speed* Time.deltaTime;
	pos.y += vAxis * speed* Time.deltaTime;
	transform.position = pos;
	
	//rotate ship dynamic movement
	transform.rotation = Quaternion.Euler(vAxis*pitchMult,hAxis*rollMult,0);
	
        
    }
}
