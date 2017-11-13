using UnityEngine;
using System.Collections;

public class Projectil_Power : MonoBehaviour {


    float power;
    GameObject catapult;
    private Rigidbody bouldRB;

    void Awake ()
    {
        bouldRB = gameObject.GetComponent<Rigidbody>();
        catapult = GameObject.Find("Catapult");
     //   AddPhysicsToBoulder();
    }

    public void AddPhysicsToBoulder (float force)
    {
        
        bouldRB.AddForce((catapult.transform.forward * -1) * force, ForceMode.Impulse);
        bouldRB.AddForce(Vector3.up * force, ForceMode.Impulse);
    }
}
