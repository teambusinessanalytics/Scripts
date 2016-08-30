using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickupParent : MonoBehaviour {

	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;

	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	void FixedUpdate () {
		device = SteamVR_Controller.Input ((int)trackedObj.index);
		if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) 
		{
			Debug.Log ("Trigger holding touch");
		}

		if(device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			Debug.Log ("Trigger touch down");
		}

		if(device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
		{
			Debug.Log ("Trigger touch up");
		}

		if(device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
		{
			Debug.Log ("Trigger holding press");
		}

		if(device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			Debug.Log ("Trigger press down");
		}

		if(device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
		{
			Debug.Log ("Trigger press up");

		}
			
	}

	void OnTriggerStay(Collider col) //col is the object that is collided with the controller
	{
		Transform originTransform = col.transform.parent;
		Debug.Log (col + "'s original transform: " + originTransform.name);
		Debug.Log ("You've collied with " + col.name + " and activated onTriggerStay");
		if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
		{
			Debug.Log ("You've collied with " + col.name + " while holding down Touch");
			//col.attachedRigidbody.isKinematic = true;
			col.gameObject.transform.SetParent (this.gameObject.transform); //attach the picked up object to the controller
		}
		if(device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
		{
			Debug.Log ("You've released Touch while colliding with " + col.name);
			col.gameObject.transform.SetParent (originTransform.transform);
			col.attachedRigidbody.isKinematic = false;

			tossObject (col.attachedRigidbody);
		}
	}

	void tossObject(Rigidbody rigidBody)
	{
		Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
		if(origin != null)
		{
			rigidBody.velocity = origin.TransformVector (device.velocity);
			rigidBody.angularVelocity = origin.TransformVector (device.angularVelocity);
		}
		else
		{
			rigidBody.velocity = device.velocity;
			rigidBody.angularVelocity = device.angularVelocity;	
		}

	}
}
