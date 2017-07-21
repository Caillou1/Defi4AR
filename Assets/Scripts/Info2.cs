using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info2 : MonoBehaviour {

    public bool OpenMenu = false;

    private InfoWindow Affiche2_win;

    // Use this for initialization
    void Start()
    {
        Affiche2_win = GameObject.Find("Affiche2").GetComponent<InfoWindow>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnBecameVisible()
    {
        Affiche2_win.OpenWindow();
        OpenMenu = true;
    }
}
