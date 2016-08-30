using UnityEngine;
using System.Collections;

public class CloseLogic : PickupLogic {

    public GameObject chart;
    public void CloseChart()
    {
        chart.SetActive(false);
    }
}
