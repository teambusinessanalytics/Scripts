using UnityEngine;
using System.Collections;
using VRTK;

public class LaptopLogic : VRTK_InteractableObject
{

    public override void OnInteractableObjectGrabbed(InteractableObjectEventArgs e)
    {
        base.OnInteractableObjectGrabbed(e);
        //gameObject.GetComponent<Collider>().isTrigger = true;
    }
    public override void OnInteractableObjectUngrabbed(InteractableObjectEventArgs e)
    {
        base.OnInteractableObjectUngrabbed(e);
        gameObject.GetComponent<Collider>().isTrigger = false;
        gameObject.GetComponent<Collider>().enabled = true;

    }

}
