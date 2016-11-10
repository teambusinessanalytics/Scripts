using UnityEngine;
using System.Collections;

public class KnobReactor : MonoBehaviour {
    public GameObject targetObject;
    
	// Use this for initialization
	void Start () {
        GetComponent<VRTK.VRTK_Knob>().defaultEvents.OnValueChanged.AddListener(handleTurn);
	}
	
    private void handleTurn(float value, float normValue)
    {
        //Debug.Log("turned: " + value + ", " + normValue);
        targetObject.transform.localEulerAngles = new Vector3(targetObject.transform.localEulerAngles.x, value, targetObject.transform.localEulerAngles.z);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
