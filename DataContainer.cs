using UnityEngine;
using System.Collections;
using VRTK;

public class DataContainer : MonoBehaviour {
    public float rotateStep = 3;

    public void RotateRight()
    {
        Debug.Log("datacontainer rotate right...");
        transform.Rotate(new Vector3(0,1,0), rotateStep, Space.World);
    }

    public void RotateLeft()
    {
        Debug.Log("datacontainer rotate left...");
        transform.Rotate(new Vector3(0, -1, 0), rotateStep, Space.World);
    }
}
