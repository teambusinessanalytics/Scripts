using UnityEngine;
using System.Collections;

public class LaptopLogic : PickupLogic {

    void Update()
    {
        
    }
    public void showChart2D()
    {
        if (AppIcon.activeInHierarchy == true)
        {
            AppIcon.SetActive(false);
            Chart2D.SetActive(true);
        }
        else
        {
            Chart2D.SetActive(true);
        }

    }
}
