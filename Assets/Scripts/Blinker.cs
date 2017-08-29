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
	private string ColorParameter;
	public bool Emit;

	private Material mat;
	private bool IsEmissiveAtStart;
	private IEnumerator BlinkRoutine;
	private bool isStarted = false;

	void Awake () {
		if (ChooseMaterialManually) {
			mat = MaterialToBlink;
		} else {
			mat = GetComponent<MeshRenderer> ().material;
		}

		if (Emit) {
			ColorParameter = "_EmissionColor";
			IsEmissiveAtStart = mat.IsKeywordEnabled ("_EMISSION");

			if (IsEmissiveAtStart) {
				BlinkColor = mat.GetColor ("_EmissionColor");
			}
		} else {
			ColorParameter = "_Color";
		}

		if (BlinkAtStart)
			StartAction ();
	}

	public override void StartAction() {
		if(Emit)
			mat.EnableKeyword("_EMISSION");
		BlinkRoutine = Blink ();
		StartCoroutine (BlinkRoutine);
		isStarted = true;
	}

	public override void StopAction() {
		if (isStarted) {
			isStarted = false;
			mat.SetColor (ColorParameter, BlinkColor);
			if (!IsEmissiveAtStart && Emit) {
				mat.DisableKeyword ("_EMISSION");
			}
			if (BlinkRoutine != null)
				StopCoroutine (BlinkRoutine);
		}
	}

	IEnumerator Blink() {
		while (true) {
			float emission = Mathf.PingPong (Time.time * BlinkSpeed, 1.0f);

			Color finalColor = BlinkColor * Mathf.LinearToGammaSpace (emission);

			mat.SetColor (ColorParameter, finalColor);

			yield return new WaitForEndOfFrame ();
		}
	}
}
