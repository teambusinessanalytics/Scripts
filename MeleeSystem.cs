using UnityEngine;
using System.Collections;

public class MeleeSystem : MonoBehaviour {

    public int Damage = 50;
    public float Distance;
    public float HarmDistance = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

	}

    public void Attack()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            Distance = hit.distance;
            if (Distance < HarmDistance)
            {
                hit.transform.SendMessage("ApplyDamage", Damage, SendMessageOptions.DontRequireReceiver);
            }
            else if (Distance > HarmDistance)
            {

            }

        }
        Debug.DrawLine(transform.position, hit.point, Color.red);
    }
}
