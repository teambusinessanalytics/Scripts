﻿using UnityEngine;
using System.Collections;
using Valve.VR;



public class ViveLaserPointer : IUILaserPointer {

    public EVRButtonId button = EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_TrackedObject _trackedObject;
    private bool _connected = false;
    private int _index;

    protected override void Initialize()
    {
        base.Initialize();
        Debug.Log("Laser Pointer Initialize");

        var trackedObject = GetComponent<SteamVR_TrackedObject>();

        if(trackedObject != null) {
            _index = (int)trackedObject.index;
            _connected = true;
        }
    }

    public override bool ButtonDown()
    {
        Debug.Log("Laser Pointer button down");
        if(!_connected)
            return false;

        var device = SteamVR_Controller.Input(_index);
        if(device != null) {
            var result = device.GetPressDown(button);
            return result;
        }

        return false;
    }

    public override bool ButtonUp()
    {
        Debug.Log("Laser Pointer button up");
        if(!_connected)
            return false;

        var device = SteamVR_Controller.Input(_index);
        if(device != null)
            return device.GetPressUp(button);

        return false;
    }

    public override void OnEnterControl(GameObject control)
    {
        Debug.Log("Laser Pointer on enter");
        var device = SteamVR_Controller.Input(_index);
        device.TriggerHapticPulse(1000);
    }

    public override void OnExitControl(GameObject control)
    {
        Debug.Log("Laser Pointer on exit");
        var device = SteamVR_Controller.Input(_index);
        device.TriggerHapticPulse(600);
    }
}

