using UnityEngine;
using System.Collections;

public class Sphere_Radius_Kin : MonoBehaviour {


    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Destroyable")
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
