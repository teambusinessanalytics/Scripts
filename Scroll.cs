using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {
    private bool isScrolling;
    private float rotation;

	// Use this for initialization
	void Start () {
        Setup();
	}
	
	// Update is called once per frame
	void Update () {
        if (isScrolling)
        {
            // Get the current transform position of the panel
            Vector3 pos = gameObject.transform.localPosition;
            Debug.Log("Current Positon: " + pos);

            //Vector3 localVectorUp = gameObject.transform.TransformDirection(0, 1, 1);
            //pos += localVectorUp * .8f * Time.deltaTime;

            // Increment the Y value of the panel 
            Vector3 _incrementYPosition =
              new Vector3(pos.x,
                          pos.y + .006f,
                          pos.z);

            // Change the transform position to the new one
            Debug.Log("New Position: " + pos);
            gameObject.transform.localPosition = _incrementYPosition;


        }
    }

    void Setup()
    {
        isScrolling = true;
        //rotation = gameObject.GetComponentInParent<Transform>().eulerAngles.x;
        //print("canvas rotation: " + rotation);
    }
}
