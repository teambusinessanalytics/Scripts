using UnityEngine;
using System.Collections;
using System;

public class WindowChangerLogic : PickupLogic {
    public Material DefaultSkybox;
    public Material ChangedSkybox;

	public void ChangeSkybox()
    {
        print("changing skybox: "+ RenderSettings.skybox.name + DefaultSkybox.name);
        RenderSettings.skybox = RenderSettings.skybox.name == DefaultSkybox.name ?ChangedSkybox:DefaultSkybox;
    }

    public override void HandlePointerInTriggerEvent()
    {
        ChangeSkybox();
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
