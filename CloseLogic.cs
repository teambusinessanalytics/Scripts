using UnityEngine;
using System.Collections;
using VRTK;

public class CloseLogic : VRTK_InteractableObject
{

    public GameObject chart;

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);
        CloseChart();
    }

    public override void StopUsing(GameObject usingObject)
    {
        base.StopUsing(usingObject);
        gameObject.SetActive(false);
    }

    public void CloseChart()
    {
        chart.SetActive(false);
    }
}
