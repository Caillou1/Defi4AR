using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Arrow : AAction {
	public GameObject ArrowHead;
	public GameObject ArrowBody;
	public Transform[] Waypoints;
	public float TimeToCompleteAnimation = 10;
	public float ScaleDownTime = 5;
	public float TimeBetweenSquares = .25f;
	public bool MoveAtStart = true;
	public bool Loops = true;

	private bool running = false;
	private Transform tf;
	private Vector3[] points;
	private IEnumerator tailroutine;
	private Tweener pathTween;
	private GameObject head;
	private Vector3 origin;

	private List<GameObject> TailObjects;

	void Start () {
		base.Start ();

		tf = transform;
		origin = tf.position;

		points = new Vector3[Waypoints.Length];
		for (int i = 0; i < Waypoints.Length; i++) {
			points [i] = Waypoints [i].position;
		}

		TailObjects = new List<GameObject> ();

		if (MoveAtStart)
			StartAction ();
	}

	public override void StartAction() {
		running = true;
		head = Instantiate (ArrowHead, tf.position, Quaternion.identity, tf);
		tailroutine = SpawnTail ();
		StartCoroutine (tailroutine);
		LaunchArrow ();
	}

	void LaunchArrow() {
		tf.position = origin;
		pathTween = tf.DOPath (points, TimeToCompleteAnimation, PathType.CatmullRom, PathMode.Full3D, 5, new Color (Random.value, Random.value, Random.value, 1f)).SetLookAt (.01f , Vector3.forward, Vector3.up).SetEase(Ease.Linear).OnComplete (() => {
			if(Loops)
				LaunchArrow();
			else
				StopAction();
		});
	}

	void Update() {
		if(head != null)
			head.transform.localRotation = Quaternion.Euler (Vector3.zero);
	}

	public override void StopAction() {
		running = false;
		tf.position = origin;

		foreach (var obj in TailObjects) {
			Destroy (obj);
		}
		TailObjects.Clear ();

		if (pathTween != null) {
			pathTween.Complete ();
			pathTween.Kill ();
		}

		if (tailroutine != null)
			StopCoroutine (tailroutine);

		if(head != null)
			Destroy (head);
	}

	IEnumerator SpawnTail() {
		while (running) {
			yield return new WaitForSeconds (TimeBetweenSquares);
			var obj = Instantiate (ArrowBody, tf.position, Quaternion.identity);
			TailObjects.Add (obj);
			obj.transform.DOScale (Vector3.zero, ScaleDownTime).OnComplete (() => {
				TailObjects.Remove(obj);
				Destroy (obj);
			});
		}
	}
}