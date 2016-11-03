using UnityEngine;
using System.Collections;
using System;

public class TowerLogic : PickupLogic {
    public Material DefaultSkybox;
    public Material ChangedSkybox;

	public void ChangeSkybox()
    {
        print("changing skybox: "+ RenderSettings.skybox.name + DefaultSkybox.name);
        RenderSettings.skybox = RenderSettings.skybox.name == DefaultSkybox.name ?ChangedSkybox:DefaultSkybox;
    }

    public override void HandlePointerInTriggerEvent()
    {
        throw new NotImplementedException();
    }

    public override void HandlePointerOutTriggerEvent()
    {
        throw new NotImplementedException();
    }

    public override void ResetAllFlipcharts()
    {
        throw new NotImplementedException();
    }
}
