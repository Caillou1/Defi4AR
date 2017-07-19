using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	private GameObject CurrentWindow;

	private GameObject[] Pages;
	private int CurrentPage;

	private Transform tf;

	void Start() {
		tf = transform;

		ShowWindow (GameObject.Find ("Window1").gameObject);
	}

	public void LoadMain() {
		SceneManager.LoadScene ("Main");
	}

	public void ShowWindow(GameObject window) {
		CurrentWindow = window;
		CurrentWindow.SetActive (true);
		var pages = CurrentWindow.transform.Find ("Pages");
		Pages = new GameObject[pages.childCount];

		for (int i = 0; i < Pages.Length; i++) {
			Pages [i] = pages.GetChild (i).gameObject;
			Pages [i].SetActive (false);
		}
		Pages [0].SetActive (true);
		CurrentPage = 0;
	}

	public void CloseWindow() {
		CurrentWindow.SetActive (false);
		CurrentWindow = null;
	}

	public void NextPage() {
		if (CurrentPage < Pages.Length - 1) {
			Pages [CurrentPage].SetActive (false);
			CurrentPage++;
			Pages [CurrentPage].SetActive (true);
		}
	}

	public void PreviousPage() {
		if (CurrentPage > 0) {
			Pages [CurrentPage].SetActive (false);
			CurrentPage--;
			Pages [CurrentPage].SetActive (true);
		}
	}
}