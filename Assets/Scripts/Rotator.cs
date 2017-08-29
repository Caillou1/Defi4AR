using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : AAction {
	public float RotationSpeed;
	public Vector3 RotationVector;

	private Transform tf;
	private bool Rotating;

	void Start() {
		tf = transform;
	}

	public override void StartAction ()
	{
		Rotating = true;	
	}

	public override void StopAction ()
	{
		Rotating = false;
	}

	void Update() {
		if(Rotating)
			tf.Rotate (Time.deltaTime * RotationVector * RotationSpeed);
	}
}
