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
		}

		if (MoveAtStart)
			StartAction ();
	}

	public override void StartAction ()
	{
		actionTweener = tf.DOPath (WaypointsPositions, TotalDuration, PathType.CatmullRom, PathMode.Full3D, 10).SetEase(Ease.Linear).OnComplete(() => {
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
