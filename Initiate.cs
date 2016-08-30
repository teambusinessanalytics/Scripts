using UnityEngine;
using System.Collections;

public static class Initiate {
//	public static void Fade (string scene,Color col,float damp){
//		GameObject init = new GameObject ();
//		init.name = "Fader";
//		init.AddComponent<Fader> ();
//		Fader scr = init.GetComponent<Fader> ();
//		scr.fadeDamp = damp;
//		scr.fadeScene = scene;
//		scr.fadeColor = col;
//		scr.start = true;
//	}
	public static void Fade (string scene,Color col, Vector3 finalPositionVector, Quaternion finalRotationVector, float damp){
//		GameObject init = new GameObject ();
//		init.name = "Fader";
//		init.AddComponent<Fader> ();

//		Fader scr = GameObject.Find ("Sphere").GetComponent<Fader> ();

				GameObject init = new GameObject ();
				init.name = "Fader";
				init.AddComponent<Fader> ();
		Fader scr = init.GetComponent<Fader> ();
		scr.fadeDamp = damp;
		scr.finalPositionVector = finalPositionVector;
		scr.finalRotationVector = finalRotationVector;
		scr.fadeScene = scene;
		scr.fadeColor = col;
		scr.start = true;
	}
}
