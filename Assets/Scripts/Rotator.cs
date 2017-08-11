using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
	public float RotationSpeed;
	public Vector3 RotationVector;

	private Transform tf;

	void Start() {
		tf = transform;
	}

	void Update() {
		tf.Rotate (Time.deltaTime * RotationVector * RotationSpeed);
	}
}
