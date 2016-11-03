using UnityEngine;
using System.Collections;

public class KnobReactor : MonoBehaviour {
    public GameObject go;
    
	// Use this for initialization
	void Start () {
        GetComponent<VRTK.VRTK_Knob>().defaultEvents.OnValueChanged.AddListener(handleTurn);
	}
	
    private void handleTurn(float value, float normValue)
    {
        //Debug.Log("turned: " + value + ", " + normValue);
        go.transform.localEulerAngles = new Vector3(go.transform.localEulerAngles.x, value, go.transform.localEulerAngles.z);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
