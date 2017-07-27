using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class WaitForTarget : MonoBehaviour {
	public GameObject Window;

	public void ShowWindow() {
		MenuManager.Instance.ShowWindow (Window);
	}

	public void HideWindow() {
		MenuManager.Instance.CloseWindow ();
	}
}
