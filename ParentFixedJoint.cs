using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ParentFixedJoint : MonoBehaviour {

	public Rigidbody rigidBodyAttachPoint;
	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;

	FixedJoint fixedJoint;


	// Use this for initialization
    

	void Awake ()
    {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		device = SteamVR_Controller.Input ((int)trackedObj.index);


		if(device.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
		{
			Debug.Log ("app menu press up");
			print("reset scene");
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

        
    }

	void OnTriggerStay(Collider col)
	{
		print ("You've collied with " + col.name + " and activated onTriggerStay");
		if((device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) && fixedJoint == null && col.name != "table1")
		{
			//print ("fixedJoint is null and deviced holding touch");
			fixedJoint = col.gameObject.AddComponent<FixedJoint> ();
			//print ("fixedJoint added");
			fixedJoint.connectedBody = rigidBodyAttachPoint;
			//print ("fixedJoint attach point assigned");
		}
        else if (fixedJoint != null && (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger) || device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)))
        {
            //print ("fixedJoint is not null and deviced realses touch");

            Rigidbody rigidBody = fixedJoint.gameObject.GetComponent<Rigidbody>();
            Destroy(fixedJoint);
            print("fixedJoint destroyed");
            fixedJoint = null;

            tossObject(rigidBody);
            print("tossing object");

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

		rigidBody.maxAngularVelocity = rigidBody.angularVelocity.magnitude * 0.3f;
	}
}
