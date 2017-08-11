using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayer : AAction {
	public Animator anim;
	public string NextParameter;

	public override void StartAction ()
	{
		if(anim != null)
			anim.SetBool (NextParameter, true);
	}

	public override void StopAction ()
	{
		if(anim != null)
			anim.SetBool (NextParameter, false);
	}
}