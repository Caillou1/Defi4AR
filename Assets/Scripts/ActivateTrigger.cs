using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrigger : AAction {
	public Animator anim;
	public string nextTrigger = "Trigger";
	public string backTrigger = "Back";

	public override void StartAction ()
	{
		anim.SetTrigger (nextTrigger);
	}

	public override void StopAction ()
	{
		anim.SetTrigger (backTrigger);
	}
}
