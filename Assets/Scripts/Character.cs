using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class Character : AAction {
	public bool UseChildrenAsWaypoints;
	[HideIf("UseChildrenAsWaypoints")]
	public Transform[] Waypoints;
	public float TotalDuration;
	public bool MoveAtStart;
	public bool IsLooping;
	public Ease EaseType;
	public Vector3 ForwardVector;

	private Vector3[] WaypointsPositions;
	private Transform tf;
	private Tweener actionTweener;
	private Vector3 originPosition;

	private SkinnedMeshRenderer smr;
	private MeshRenderer mr;
	private bool running;

	void Start () {
		tf = transform;
		originPosition = tf.position;

		if (UseChildrenAsWaypoints) {
			WaypointsPositions = new Vector3[tf.childCount];

			for (int i = 0; i < tf.childCount; i++) {
				WaypointsPositions [i] = tf.GetChild (i).position;
			}
		} else {
			WaypointsPositions = new Vector3[Waypoints.Length];

			for (int i = 0; i < Waypoints.Length; i++) {
				WaypointsPositions[i] = Waypoints [i].position;
			}
		}

		mr = tf.GetComponentInChildren<MeshRenderer> ();
		smr = tf.GetComponentInChildren<SkinnedMeshRenderer> ();

		if (MoveAtStart)
			StartAction ();
	}

	public override void StartAction ()
	{
		if (!running) {
			running = true;
			StartCoroutine (CheckDisabled ());
		}
		tf.position = originPosition;
		actionTweener = tf.DOPath (WaypointsPositions, TotalDuration, PathType.CatmullRom, PathMode.Full3D, 10).SetLookAt (.01f , ForwardVector, Vector3.up).SetEase(EaseType).OnComplete(() => {
			if(IsLooping && running) {
				StartAction();
			} else {
				Stop();
			}
		});
	}

	public override void StopAction ()
	{
		/*if (actionTweener != null) {
			running = false;
			actionTweener.Kill ();
			tf.position = originPosition;
		}*/
	}

	void Stop() {
		if (actionTweener != null) {
			running = false;
			actionTweener.Kill ();
			tf.position = originPosition;
		}
	}

	IEnumerator CheckDisabled() {
		while (running) {
			if (smr != null) {
				if (!smr.enabled) {
					Stop ();
				}
			}

			if (mr != null) {
				if (!mr.enabled) {
					Stop ();
				}
			}
			yield return new WaitForEndOfFrame ();
		}
	}
}
