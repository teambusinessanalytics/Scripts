﻿using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using VRTK;
using UnityEngine.SceneManagement;

public class PointerEventCaller : MonoBehaviour {

    private VRTK_SimplePointer simplePointer;
    private VRTK_ControllerEvents controllerEvents;
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    bool dosomething;
    private ExhibitorLogic exhibitor;

    void Awake()
    {
        simplePointer = GetComponent<VRTK_SimplePointer>();
        controllerEvents = GetComponent<VRTK_ControllerEvents>();
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void OnEnable()
    {
        
        controllerEvents.TouchpadPressed += HandleTouchPadPressed;
        controllerEvents.TouchpadReleased += HandleTouchPadReleased;
    }

    
    private void HandleTouchPadPressed(object sender, ControllerInteractionEventArgs e)
    {
        simplePointer.ToggleBeam(true);
        controllerEvents.TriggerClicked += HandleTriggerPressed;
        controllerEvents.TriggerUnclicked += HandleTriggerReleased;
    }

    private void HandleTouchPadReleased(object sender, ControllerInteractionEventArgs e)
    {
        simplePointer.ToggleBeam(false);
        controllerEvents.TriggerClicked -= HandleTriggerPressed;
        controllerEvents.TriggerUnclicked -= HandleTriggerReleased;
    }

    void OnDisable()
    {
        
        controllerEvents.TouchpadPressed -= HandleTouchPadPressed;
        controllerEvents.TouchpadReleased -= HandleTouchPadReleased;
    }

    void FixedUpdate()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);


        if (device.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            Debug.Log("app menu press up");
            print("reset scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    private void HandleTriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("trigger pressed event from caller...");
        simplePointer.DestinationMarkerEnter += HandlePointerIn;
        simplePointer.DestinationMarkerExit += HandlePointerOut;
    }

    private void HandleTriggerReleased(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("trigger released event from caller.....");
        simplePointer.DestinationMarkerEnter -= HandlePointerIn;
        simplePointer.DestinationMarkerExit -= HandlePointerOut;
    }


    private void HandlePointerIn(object sender, DestinationMarkerEventArgs e)
    {
        
        Debug.Log("shooting " + e.target.name + " while pointer in");
        Debug.Log("trigger flipchart...");
        exhibitor = e.target.gameObject.GetComponent<ExhibitorLogic>();
        if (exhibitor)
        {
            exhibitor.toggleChart3D();
        }else
        {
            exhibitor = new ExhibitorLogic();
            exhibitor.ResetAllFlipcharts();
        }

        //unassign the event to avoid repeat of the actions above
        simplePointer.DestinationMarkerEnter -= HandlePointerIn;
        

    }

    private void HandlePointerOut(object sender, DestinationMarkerEventArgs e)
    {
        exhibitor.showChart3D(false);
        Debug.Log("stopped shooting " + e.target.name);
        
    }
}
