using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
	private GameObject CurrentWindow;
	private List<List<GameObject>> Pages;
	private int CurrentPage;
	private int CurrentParagraphe;
	private Transform tf;
	private List<AAction> ActionsList;
	private GameObject PreviousButton;
	private GameObject NextButton;

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

	public void LoadMenu() {
		SceneManager.LoadScene ("Menu");
	}

	public void ShowWindow(GameObject window) {
		if (CurrentWindow != window) {
			CloseWindow ();
			CurrentWindow = window;
			CurrentWindow.SetActive (true);
			PreviousButton = CurrentWindow.transform.Find ("LeftArrow").gameObject;
			NextButton = CurrentWindow.transform.Find ("RightArrow").gameObject;
			var pages = CurrentWindow.transform.Find ("Pages");
			Pages = new List<List<GameObject>> ();

			foreach (var action in CurrentWindow.GetComponentsInChildren<ActionList>())
				action.StopActions ();

			for (int i = 1; i <= pages.childCount - 1; i++) {
				var p = pages.GetChild (i);
				Pages.Add (new List<GameObject> ());
				for (int j = 0; j < p.childCount; j++) {
					var tmp = p.GetChild (j).gameObject;
					tmp.SetActive (false);
					Pages [i - 1].Add (tmp);
				}
			}

			Pages [0][0].SetActive (true);

			var al = Pages [0][0].GetComponent<ActionList> ();
			if (al != null)
				al.StartActions ();
			
			CurrentPage = 0;
			CurrentParagraphe = 0;

			PreviousButton.SetActive (false);
			NextButton.SetActive (Pages.Count > 1 || Pages[0].Count > 1);
		} else {
			var al = Pages [CurrentPage][CurrentParagraphe].GetComponent<ActionList> ();
			if(al != null) al.StartActions ();
		}
	}

	public void CloseWindow() {
		if (CurrentWindow != null) {
			var al = Pages [CurrentPage][CurrentParagraphe].GetComponent<ActionList> ();
			if (al != null)
				al.StopActions ();
			
			CurrentWindow.SetActive (false);
			CurrentWindow = null;
		}
	}

	public void NextPage() {
		var al = Pages [CurrentPage][CurrentParagraphe].GetComponent<ActionList> ();
		if (al != null) {
			al.StopActions ();
		}

		if (CurrentParagraphe < Pages [CurrentPage].Count - 1) {
			CurrentParagraphe++;
		} else if (CurrentPage < Pages.Count - 1) {
			foreach (var obj in Pages[CurrentPage])
				obj.SetActive (false);

			CurrentPage++;
			CurrentParagraphe = 0;
		}

		Pages [CurrentPage] [CurrentParagraphe].SetActive (true);

		if (CurrentPage == Pages.Count - 1 && CurrentParagraphe == Pages [CurrentPage].Count - 1)
			NextButton.SetActive (false);
		else
			NextButton.SetActive (true);

		if (CurrentPage == 0 && CurrentParagraphe == 0)
			PreviousButton.SetActive (false);
		else
			PreviousButton.SetActive (true);

		var al2 = Pages [CurrentPage] [CurrentParagraphe].GetComponent<ActionList> ();
		if (al2 != null)
			al2.StartActions ();
	}

	public void PreviousPage() {
		var al = Pages [CurrentPage] [CurrentParagraphe].GetComponent<ActionList> ();
		if (al != null) {
			al.StopActions ();
		}

		Pages [CurrentPage] [CurrentParagraphe].SetActive (false);

		if (CurrentParagraphe > 0) {
			CurrentParagraphe--;
		} else if (CurrentPage > 0) {
			foreach (var obj in Pages[CurrentPage])
				obj.SetActive (false);

			CurrentPage--;
			CurrentParagraphe = 0;
		}

		Pages [CurrentPage] [CurrentParagraphe].SetActive (true);

		if (CurrentPage == Pages.Count - 1 && CurrentParagraphe == Pages [CurrentPage].Count - 1)
			NextButton.SetActive (false);
		else
			NextButton.SetActive (true);

		if (CurrentPage == 0 && CurrentParagraphe == 0)
			PreviousButton.SetActive (false);
		else
			PreviousButton.SetActive (true);

		var al2 = Pages [CurrentPage][CurrentParagraphe].GetComponent<ActionList> ();
		if (al2 != null)
			al2.StartActions ();
	}
}