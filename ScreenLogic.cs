using UnityEngine;
using System.Collections;
using System;

public class ScreenLogic : PickupLogic {
    public GameObject Chart3D;

    public override void HandlePointerInTriggerEvent()
    {

    }

    public override void HandlePointerOutTriggerEvent()
    {
        
    }

    public override void ResetAllFlipcharts()
    {
        
    }

    public void showChart3D()
    {
        if (Chart3D.activeInHierarchy == false)
        {
            Chart3D.SetActive(true);
            Chart3D.GetComponent<Animator>().Play("ChartEvo");
        }
    }
}
