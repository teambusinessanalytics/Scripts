using UnityEngine;
using System.Collections;

public class pickupSwitching : MonoBehaviour {
    public GameObject PickupCube;
    public GameObject OfficeObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SwapObject()
    {
        if (PickupCube.activeInHierarchy==true)
        {
            PickupCube.SetActive(false);
            OfficeObject.SetActive(true);
        }else
        {
            PickupCube.SetActive(true);
            OfficeObject.SetActive(false);

        }
    }
}
