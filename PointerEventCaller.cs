using UnityEngine;
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
        controllerEvents.TriggerClicked += HandleTriggerPressed;
        controllerEvents.TriggerUnclicked += HandleTriggerReleased;
    }

    void OnDisable()
    {
        controllerEvents.TriggerClicked -= HandleTriggerPressed;
        controllerEvents.TriggerUnclicked -= HandleTriggerReleased;
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
        exhibitor = e.target.gameObject.GetComponent<ExhibitorLogic>();
        Debug.Log("trigger flipchart...");
        if (exhibitor)
        {
            exhibitor.showChart3D(true);
        }else
        {
            exhibitor = new ExhibitorLogic();
            exhibitor.ResetAllFlipcharts();
        }
        

    }

    private void HandlePointerOut(object sender, DestinationMarkerEventArgs e)
    {
        exhibitor.showChart3D(false);
        Debug.Log("stopped shooting " + e.target.name);
        
    }
}
