using UnityEngine;
using System.Collections;
using VRTK;

public class Exhibitor1 : VRTK_InteractableObject
{
    ExhibitorLogic exhibitor;
    bool onUsing;

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);
        exhibitor.ChangedMaterial(true);
        onUsing = true;
    }

    public override void StopUsing(GameObject usingObject)
    {
        base.StopUsing(usingObject);
        exhibitor.ChangedMaterial(false);

    }


    // Use this for initialization
    void Start () {
        onUsing = false;
        base.Start();
        exhibitor = transform.GetComponent<ExhibitorLogic>();
	}
	
	// Update is called once per frame
	void Update () {
        if (onUsing)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("1 pressed");
                exhibitor.toggleChart3D();
            }
        }
        
	
	}

}
