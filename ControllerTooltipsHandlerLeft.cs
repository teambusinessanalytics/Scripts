using UnityEngine;
using System.Collections;
using VRTK;

public class ControllerTooltipsHandlerLeft : MonoBehaviour
{
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device rightDevice;
    SteamVR_Controller.Device leftDevice;
    private VRTK_ControllerTooltips tooltip;
    private bool tooltipOn;

    // Use this for initialization
    void Awake()
    {
        tooltip = GetComponentInChildren<VRTK_ControllerTooltips>();
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        tooltipOn = true;
        rightDevice = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost));
        leftDevice = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost));
    }

    // Update is called once per frame
    void Update()
    {
        if (tooltip)
        {
            if (rightDevice.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
            {
                Debug.Log("app menu press up");
                print("toggle tooltips");

                tooltip.ToggleTips(!tooltipOn, VRTK_ControllerTooltips.TooltipButtons.TriggerTooltip);
                tooltip.ToggleTips(!tooltipOn, VRTK_ControllerTooltips.TooltipButtons.GripTooltip);

                tooltipOn = !tooltipOn;
            }
        }
    }
}
