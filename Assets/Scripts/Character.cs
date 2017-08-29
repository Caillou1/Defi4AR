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

		if (MoveAtStart)
			StartAction ();
	}

	public override void StartAction ()
	{
		tf.position = originPosition;
		actionTweener = tf.DOPath (WaypointsPositions, TotalDuration, PathType.CatmullRom, PathMode.Full3D, 10).SetLookAt (.01f , ForwardVector, Vector3.up).SetEase(EaseType).OnComplete(() => {
			if(IsLooping) {
				StartAction();
			} else {
				StopAction();
			}
		});
	}

	public override void StopAction ()
	{
		if (actionTweener != null) {
			actionTweener.Complete ();
			tf.position = originPosition;
		}
	}
}
