using UnityEngine;
using System.Collections;

public class Boulder_collision : MonoBehaviour {

    Rotation_Arm rotArm;
    SFX_main sfx;

    void Start ()
    {
        sfx = GameObject.Find("Game_Manager").GetComponent<SFX_main>();
        rotArm = GameObject.Find("catapult_arm").GetComponent<Rotation_Arm>();
        float randomNumber1 = Random.Range(0.5f, 1f);
        float randomNumber2 = Random.Range(-0.5f, 0.5f);
        float randomNumber3 = Random.Range(-0.5f, 0.5f);
        gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(randomNumber1, randomNumber2, randomNumber3), ForceMode.Impulse);
    }


    void OnCollisionEnter (Collision other)
    {
        // Debug.Log("nopeus OSUESSA " + gameObject.GetComponent<Rigidbody>().velocity.magnitude);
        if (gameObject.transform.GetChildCount() > 0)
        {
            Transform parentT = GameObject.Find("Trail_Rends").GetComponent<Transform>();
            gameObject.transform.GetChild(0).transform.SetParent(parentT);
        }
       
      
        if (other.gameObject.tag == "Destroyable" && gameObject.GetComponent<Rigidbody>().velocity.magnitude > 5)
        {
            sfx.PlaySFX(2, 1.0f);
            other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(gameObject.GetComponent<Rigidbody>().velocity.magnitude * 100, gameObject.transform.position, 4);
            //    Debug.Log("collision succesful");
            StartCoroutine(other.gameObject.GetComponent<TriangleExplosion>().SplitMesh(true));
        }

        StartCoroutine(WaitTime());
      
    }


    IEnumerator WaitTime ()
    {
        yield return new WaitForSeconds(2);
        rotArm.isBoulderDestroyed = true;
        Destroy(this.gameObject);
    }
}
