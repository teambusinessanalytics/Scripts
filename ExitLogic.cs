using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitLogic : PickupLogic {
    public override void StartUsing(GameObject currentUsingObject)
    {
        base.StartUsing(currentUsingObject);
        ChangeMaterial(true);

    }
    public override void StopUsing(GameObject previousUsingObject)
    {
        base.StopUsing(previousUsingObject);
        ChangeMaterial(false);

    }

    public override void HandlePointerInTriggerEvent()
    {
        //ResetScene();
    }
}
