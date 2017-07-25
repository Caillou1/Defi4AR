using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAction : MonoBehaviour {
	public abstract void StartAction ();
	public abstract void StopAction ();

	public virtual void Start() {
		MenuManager.Instance.RegisterAction (this);
	}

	public virtual void OnDestroy() {
		StopAction ();
		MenuManager.Instance.UnregisterAction (this);
	}
}
