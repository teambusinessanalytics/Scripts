using UnityEngine;
using System.Collections;

public class ScreenLogic : PickupLogic {
    public GameObject Chart3D;


    public void showChart3D()
    {
        if (Chart3D.activeInHierarchy == false)
        {
            Chart3D.SetActive(true);
            Chart3D.GetComponent<Animator>().Play("ChartEvo");
        }
    }
}
