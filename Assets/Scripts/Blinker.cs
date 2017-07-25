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
	private bool IsEmissiveAtStart;
	private IEnumerator BlinkRoutine;

	void Start () {
		base.Start ();

		if (ChooseMaterialManually) {
			mat = MaterialToBlink;
		} else {
			mat = GetComponent<MeshRenderer> ().material;
		}

		IsEmissiveAtStart = mat.IsKeywordEnabled ("_EMISSION");

		if (IsEmissiveAtStart) {
			BlinkColor = mat.GetColor ("_EmissionColor");
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
		if (!IsEmissiveAtStart)
			mat.DisableKeyword ("_EMISSION");
		else
			mat.SetColor ("_EmissionColor", BlinkColor);
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
