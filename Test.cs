using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	public string scene;
	public Color myColor;
	public float damp;
	public Vector3 finalPositionVector;
	public Quaternion finalRotationVector;
	// Use this for initialization

	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (GUI.Button (new Rect (0, 0, 100, 30), "Start")) {
			Initiate.Fade(scene,myColor,finalPositionVector,finalRotationVector,0.5f);	
		}
	}
}
