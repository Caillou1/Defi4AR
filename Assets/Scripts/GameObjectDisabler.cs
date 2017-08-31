using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameObjectDisabler : AAction {
	public List<GameObject> ToDisable;
	public float TimeBetweenDisable;
	public bool ExecOnStart;
	public bool DontStop;
	public bool DontScale;

	private List<Vector3> originalScales;
	private IEnumerator actionCoroutine;
	private bool running;

	void Start() {
		if (!DontScale) {
			originalScales = new List<Vector3> ();

			foreach (var obj in ToDisable) {
				originalScales.Add (obj.transform.localScale);
			}
		}

		if (ExecOnStart) {
			StartAction ();
		}
	}

	public override void StartAction ()
	{
		foreach (var o in ToDisable)
			o.SetActive (true);
		running = true;
		actionCoroutine = Disabler ();
		StartCoroutine (actionCoroutine);
	}

	public override void StopAction ()
	{
		if (actionCoroutine != null && running && !DontStop) {
			running = false;
			StopCoroutine (actionCoroutine);
			actionCoroutine = null;
		}
	}

	IEnumerator Disabler() {
		while (running) {
			foreach (var obj in ToDisable) {
				yield return new WaitForSeconds (TimeBetweenDisable);

				if (DontScale) {
					obj.SetActive (false);
				} else {
					obj.transform.DOScale (Vector3.zero, TimeBetweenDisable/2f).OnComplete (() => obj.SetActive (false));
				}
			}

			yield return new WaitForSeconds (TimeBetweenDisable);

			foreach (var obj in ToDisable) {
				obj.SetActive (true);
				if (!DontScale) {
					obj.transform.DOScale (originalScales [ToDisable.FindIndex (x => x == obj)], TimeBetweenDisable/2f);
				}
			}
		}
	}
}
