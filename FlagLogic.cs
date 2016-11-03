using UnityEngine;
using System.Collections;
using System;

public class FlagLogic : PickupLogic {

	public void EnableWalk()
    {
        print("enabling walk");
        //GameObject.Find("CardboardMain").GetComponent<Walk>().enabled = true;
    }

    public override void HandlePointerInTriggerEvent()
    {
    }

    public override void HandlePointerOutTriggerEvent()
    {
    }

    public override void ResetAllFlipcharts()
    {
    }
}
