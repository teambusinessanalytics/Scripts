﻿using UnityEngine;
using System.Collections;
using VRTK;

public class Papers : PickupLogic
{
    public GameObject laptop;
    public float lerpTime;

    private bool move = false;


    new void Update()
    {
        base.Update();
        if (move)
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            transform.position = Vector3.Lerp(originalTransform.position, laptop.transform.position, Time.deltaTime / lerpTime);
        }
    }

    public override void HandlePointerInTriggerEvent()
    {
        ObjectEvo();
    }

    public override void HandlePointerOutTriggerEvent()
    {
        ChangeMaterial(false);
    }

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

    public override void StartTouching(GameObject currentTouchingObject)
    {
        base.StartTouching(currentTouchingObject);
        animator.enabled = false;
    }

    public override void StopTouching(GameObject previousTouchingObject)
    {
        base.StopTouching(previousTouchingObject);
        animator.enabled = true;
    }
    public override void ObjectEvo()
    {
        base.ObjectEvo();
        move = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("paper collided with: " + other.gameObject.name);
        if (other.gameObject.name == laptop.name)
        {
            move = false;
            gameObject.SetActive(false);
            ActivateAppIcon();


        }
    }
}
