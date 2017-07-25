using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	private GameObject CurrentWindow;
	private GameObject[] Pages;
	private int CurrentPage;
	private Transform tf;
	private List<AAction> ActionsList;

	public static MenuManager Instance;

	void Awake() {
		Instance = this;
		tf = transform;
		ActionsList = new List<AAction> ();
	}

	public void RegisterAction(AAction a) {
		ActionsList.Add (a);
	}

	public void UnregisterAction(AAction a) {
		ActionsList.Remove (a);
	}

	public void StopAllActions() {
		foreach (var a in ActionsList) {
			a.StopAction ();
		}
	}

	public void LoadMain() {
		SceneManager.LoadScene ("Main");
	}

	public void ShowWindow(GameObject window) {
		if (CurrentWindow != window) {
			CurrentWindow = window;
			CurrentWindow.SetActive (true);
			var pages = CurrentWindow.transform.Find ("Pages");
			Pages = new GameObject[pages.childCount - 1];

			foreach (var action in CurrentWindow.GetComponentsInChildren<ActionList>())
				action.StopActions ();

			for (int i = 1; i <= Pages.Length; i++) {
				Pages [i - 1] = pages.GetChild (i).gameObject;
				Pages [i - 1].SetActive (false);
			}
			Pages [0].SetActive (true);
			var al = Pages [0].GetComponent<ActionList> ();
			if (al != null)
				al.StartActions ();
			CurrentPage = 0;
		}
	}

	public void CloseWindow() {
		CurrentWindow.SetActive (false);
		CurrentWindow = null;
	}

	public void NextPage() {
		if (CurrentPage < Pages.Length - 1) {
			if (CurrentPage > 0) {
				var al = Pages [CurrentPage].GetComponent<ActionList> ();
				if (al != null) {
					al.StopActions ();
				}
			}

			CurrentPage++;
			Pages [CurrentPage].SetActive (true);

			var al2 = Pages [CurrentPage].GetComponent<ActionList> ();
			if (al2 != null)
				al2.StartActions ();
		}
	}

	public void PreviousPage() {
		if (CurrentPage > 0) {
			if (CurrentPage < Pages.Length) {
				var al = Pages [CurrentPage].GetComponent<ActionList> ();
				if (al != null) {
					al.StopActions ();
				}
			}

			Pages [CurrentPage].SetActive (false);
			CurrentPage--;

			var al2 = Pages [CurrentPage].GetComponent<ActionList> ();
			if (al2 != null)
				al2.StartActions ();
		}
	}

	public void PreviousPage(bool DisableNextPages) {
		if (DisableNextPages)
			PreviousPage ();
		else {
			if (CurrentPage > 0) {
				if (CurrentPage < Pages.Length) {
					var al = Pages [CurrentPage].GetComponent<ActionList> ();
					if (al != null) {
						al.StopActions ();
					}
				}

				CurrentPage--;

				var al2 = Pages [CurrentPage].GetComponent<ActionList> ();
				if (al2 != null)
					al2.StartActions ();
			}
		}
	}
}