using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraType : MonoBehaviour {
	private Camera cam;
	private int TunnelingCulling;
	private int ARCulling;

	public GameObject ARCam;
	public GameObject ProjectPlan;

	private RenderTextureToProjector rttp;

	public static ChangeCameraType Instance;

	void Awake() {
		Instance = this;
		cam = GetComponent<Camera> ();

		rttp = GetComponent<RenderTextureToProjector> ();
		rttp.enabled = false;

		ARCulling = cam.cullingMask;
		TunnelingCulling = 256;
	}

	public void SetToTunnel() {
		cam.cullingMask = TunnelingCulling;
		ARCam.SetActive (true);
		ARCam.GetComponent<Camera> ().enabled = true;
		ProjectPlan.SetActive (true);
		rttp.enabled = true;
		Camera.SetupCurrent (cam);
	}

	public void SetToAR() {
		cam.cullingMask = ARCulling;
		ARCam.GetComponent<Camera> ().enabled = false;
		ARCam.SetActive (false);
		ProjectPlan.SetActive (false);
		rttp.enabled = false;
		Camera.SetupCurrent (cam);
	}
}