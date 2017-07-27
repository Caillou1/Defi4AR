using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Presentation : AAction {
	public float TimeBetween = 1f;
	public bool ShowAtStart = true;
	public bool BackAndForth = true;
	public float ScaleDuration = .5f;

	private int currentChild;
	private int previousChild;
	private int adder;
	private GameObject[] children;
	private IEnumerator presentationRoutine;

	void Start () {
		base.Start ();

		children = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; i++) {
			children [i] = transform.GetChild (i).gameObject;
			children [i].SetActive (false);
		}

		currentChild = 0;
		previousChild = -1;
		adder = 1;

		if (ShowAtStart)
			StartAction ();
	}

	public override void StartAction() {
		StopAction ();
		presentationRoutine = ShowNext ();
		StartCoroutine (presentationRoutine);
	}

	public override void StopAction() {
		if (presentationRoutine != null)
			StopCoroutine (presentationRoutine);
	}

	public void Next() {
		if(currentChild < children.Length - 1) {
			previousChild = currentChild;
			DisableChild (previousChild);
			currentChild ++;
			EnableChild (currentChild);
		}
	}

	public void Previous() {
		if(currentChild > 0) {
			previousChild = currentChild;
			DisableChild (previousChild);
			currentChild --;
			EnableChild (currentChild);
		}
	}

	void EnableChild(int i) {
		var tf = children [i].transform;
		var scale = tf.localScale;
		tf.localScale = Vector3.zero;

		children [i].transform.DOScale (scale, ScaleDuration);
		children [i].SetActive (true);
	}

	void DisableChild(int i) {
		var tf = children [i].transform;
		var scale = tf.localScale;

		children [i].transform.DOScale (Vector3.zero, ScaleDuration).OnComplete(() => {
			children [i].SetActive (false);
			tf.localScale = scale;
		});
	}

	IEnumerator ShowNext() {
		if (previousChild > -1)
			DisableChild (previousChild);

		EnableChild (currentChild);

		yield return new WaitForSeconds (TimeBetween);

		previousChild = currentChild;

		if (adder > 0) {
			if (currentChild >= children.Length - 1) {
				if (BackAndForth) {
					adder = -1;
				} else {
					currentChild = -1;
				}
			}
		} else if (adder < 0) {
			if (currentChild <= 0) {
				adder = 1;
			}
		}

		currentChild += adder;
		presentationRoutine = ShowNext ();
		StartCoroutine (presentationRoutine);
	}
}
