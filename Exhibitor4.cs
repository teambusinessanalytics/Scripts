using UnityEngine;
using System.Collections;
using VRTK;

public class Exhibitor4 : VRTK_InteractableObject
{

    ExhibitorLogic exhibitor;

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);
        exhibitor.ChangeMaterial(true);
    }

    public override void StopUsing(GameObject usingObject)
    {
        base.StopUsing(usingObject);
        exhibitor.ChangeMaterial(false);

    }

    // Use this for initialization
    new void Start () {
        base.Start();
        exhibitor = transform.GetComponent<ExhibitorLogic>();
    }
	
	// Update is called once per frame
	new void Update () {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            exhibitor.ToggleChart3D();
        }
    }
}
