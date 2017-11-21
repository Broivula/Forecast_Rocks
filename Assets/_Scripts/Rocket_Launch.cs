using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_Launch : MonoBehaviour {

    private bool grounded = false;
    public int rocketTimer = 20;
    private Quaternion origRot;
    Score_keeper score;

    private void Awake()
    {
        origRot = gameObject.transform.localRotation;
        score = GameObject.Find("Game_Manager").GetComponent<Score_keeper>();
    }




    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground" && !grounded)
        {
            grounded = true;
            //aloita rakettiäänet
            StartCoroutine(LaunchRocket());
            //anna pelaajalle x määrä pisteitä
            score.AddPoints(1500, gameObject);
        }
    }

    IEnumerator LaunchRocket ()
    {
        Rigidbody rocketRB = gameObject.GetComponent<Rigidbody>();
        rocketRB.isKinematic = true;
        rocketRB.isKinematic = false;
        transform.rotation = origRot;
        for(int i = 0; i < rocketTimer;i++)
        {
            rocketRB.AddForce(Vector3.up, ForceMode.Impulse);
            yield return new WaitForEndOfFrame();
        }
    }

}
