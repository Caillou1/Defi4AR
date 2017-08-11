using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class WaitForTarget : MonoBehaviour {
	public GameObject Window;

	void Start() {
		HideLights ();
	}

	public void ShowWindow() {
		MenuManager.Instance.ShowWindow (Window);
		foreach (var l in GetComponentsInChildren<Light>()) {
			l.enabled = true;
		}
	}

	public void HideWindow() {
		MenuManager.Instance.CloseWindow ();
		HideLights ();
	}

	public void HideLights() {
		foreach (var l in GetComponentsInChildren<Light>()) {
			l.enabled = false;
		}
	}
}
