using UnityEngine;
using System.Collections;
using VRTK;

public class Exhibitor3 : VRTK_InteractableObject
{
    ExhibitorLogic exhibitor;

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);
        exhibitor.toggleChart3D();
    }

    public override void StopUsing(GameObject usingObject)
    {
        base.StopUsing(usingObject);
        exhibitor.toggleChart3D();
    }

    // Use this for initialization
    void Start () {
        exhibitor = transform.GetComponent<ExhibitorLogic>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            exhibitor.toggleChart3D();
        }
    }
}
