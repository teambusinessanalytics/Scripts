﻿using UnityEngine;
using System.Collections;

public class Exhibitor3 : MonoBehaviour {
    ExhibitorLogic exhibitor;

    // Use this for initialization
    void Start () {
        exhibitor = transform.GetComponent<ExhibitorLogic>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            exhibitor.showChart3D();
        }
    }
}
