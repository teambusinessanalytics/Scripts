using UnityEngine;
using System.Collections;

public class TowerLogic : PickupLogic {
    public Material DefaultSkybox;
    public Material ChangedSkybox;

	public void ChangeSkybox()
    {
        print("changing skybox: "+ RenderSettings.skybox.name + DefaultSkybox.name);
        RenderSettings.skybox = RenderSettings.skybox.name == DefaultSkybox.name ?ChangedSkybox:DefaultSkybox;
    }
}
