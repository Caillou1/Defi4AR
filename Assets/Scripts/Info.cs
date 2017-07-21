using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour {

    public bool OpenMenu = false;

    private InfoWindow Affiche1_win;

	// Use this for initialization
	void Start () {
        Affiche1_win = GameObject.Find("Affiche1").GetComponent<InfoWindow>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnBecameVisible()
    {
        Affiche1_win.OpenWindow();
        OpenMenu = true;
    }
}
