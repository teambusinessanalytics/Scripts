using UnityEngine;
using System.Collections;

public class Exhibitor1 : MonoBehaviour {
    ExhibitorLogic exhibitor;

    // Use this for initialization
    void Start () {
        exhibitor = transform.GetComponent<ExhibitorLogic>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1 pressed");
            exhibitor.showChart3D();
        }
	
	}
}
