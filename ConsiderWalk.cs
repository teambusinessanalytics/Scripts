using UnityEngine;
using System.Collections;
using System;

public class ConsiderWalk : MonoBehaviour {
 //   private bool isWalking;
 //   public float distance;
 //   public float minDistance = 3;
 //   public GameObject walkingObject;
 //   // Use this for initialization
 //   void Start () {
	
	//}
	
	//// Update is called once per frame
	//void Update () {
 //       if (Cardboard.SDK.Triggered)
 //       {
 //           RaycastHit hit = new RaycastHit();
 //           if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
 //           {
 //               distance = hit.distance;
 //               print(hit.collider.gameObject.name);
 //               string[] walkableObjects = { "Floor", "wall1", "wall2", "wall3", "wall4" };
 //               if (distance > minDistance && Array.Exists(walkableObjects, delegate(string s) { return s.Equals(hit.collider.gameObject.name); }))
 //               {
 //                   walkingObject.transform.SendMessage("Walking", SendMessageOptions.DontRequireReceiver);
 //               }
 //               Debug.DrawLine(transform.position, hit.point, Color.red);
 //           }else
 //           {
 //               walkingObject.transform.SendMessage("Walking", SendMessageOptions.DontRequireReceiver);
 //           }
 //       }
 //   }
}
