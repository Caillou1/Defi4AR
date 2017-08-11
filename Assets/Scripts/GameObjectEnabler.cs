using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameObjectEnabler : AAction {
	public List<GameObject> ToEnable;
	public List<GameObject> ToDisable;

	public override void StartAction ()
	{
		foreach (var o in ToEnable) {
			o.SetActive (true);
		}

		foreach (var o in ToDisable) {
			o.SetActive (false);
		}
	}

	public override void StopAction ()
	{
		/*foreach (var o in ToEnable)
			o.SetActive (false);
		foreach (var o in ToDisable)
			o.SetActive (true);*/
	}
}
