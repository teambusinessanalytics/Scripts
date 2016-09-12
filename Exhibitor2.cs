using UnityEngine;
using System.Collections;
using VRTK;

public class Exhibitor2 : VRTK_InteractableObject
{
    ExhibitorLogic exhibitor;

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);
        exhibitor.ChangedMaterial(true);
    }

    public override void StopUsing(GameObject usingObject)
    {
        base.StopUsing(usingObject);
        exhibitor.ChangedMaterial(false);

    }

    // Use this for initialization
    void Start () {
        exhibitor = transform.GetComponent<ExhibitorLogic>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            exhibitor.ToggleChart3D();
        }
    }
}
