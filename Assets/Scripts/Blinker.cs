using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Blinker : AAction {
	public bool ChooseMaterialManually;
	[ShowIf("ChooseMaterialManually")]
	public Material MaterialToBlink;
	public float BlinkSpeed = 3f;
	public Color BlinkColor = Color.white;
	public bool BlinkAtStart = true;

	private Material mat;

	private IEnumerator BlinkRoutine;

	void Start () {
		base.Start ();

		if (ChooseMaterialManually) {
			mat = MaterialToBlink;
		} else {
			mat = GetComponent<MeshRenderer> ().material;
		}

		if (BlinkAtStart)
			StartAction ();
	}

	public override void StartAction() {
		mat.EnableKeyword("_EMISSION");
		BlinkRoutine = Blink ();
		StartCoroutine (BlinkRoutine);
	}

	public override void StopAction() {
		mat.DisableKeyword ("_EMISSION");
		if (BlinkRoutine != null)
			StopCoroutine (BlinkRoutine);
	}

	IEnumerator Blink() {
		while (true) {
			float emission = Mathf.PingPong (Time.time * BlinkSpeed, 1.0f);

			Color finalColor = BlinkColor * Mathf.LinearToGammaSpace (emission);

			mat.SetColor ("_EmissionColor", finalColor);

			yield return new WaitForEndOfFrame ();
		}
	}
}
