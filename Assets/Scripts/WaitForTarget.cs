using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class WaitForTarget : MonoBehaviour {
	public GameObject Window;
	public bool IsTunnel;

	public void ShowWindow() {
		if (IsTunnel) {
			ChangeCameraType.Instance.SetToTunnel ();
		}

		MenuManager.Instance.ShowWindow (Window);
	}

	public void WasDisabled() {
		if (IsTunnel) {
			ChangeCameraType.Instance.SetToAR ();
		}
	}

	public void HideWindow() {
		MenuManager.Instance.CloseWindow ();
	}
}
