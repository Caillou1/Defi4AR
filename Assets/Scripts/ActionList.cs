using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionList : MonoBehaviour {
	public List<AAction> ActionsList;

	public void StartActions() {
		foreach (var a in ActionsList)
			a.StartAction ();
	}

	public void StopActions() {
		foreach (var a in ActionsList)
			a.StopAction ();
	}
}
