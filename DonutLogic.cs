using UnityEngine;
using System.Collections;

public class DonutLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void JumpIntoLaptop()
    {
        GetComponent<Animator>().Play("DonutEvo");
    }
}
