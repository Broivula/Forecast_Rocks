using UnityEngine;
using System.Collections;

public class VelocityDestroy : MonoBehaviour {

    SFX_main sfx;

    void Awake ()
    {
        sfx = GameObject.Find("Game_Manager").GetComponent<SFX_main>();
    }

    void OnCollisionEnter(Collision other)
    {
        if(gameObject.GetComponent<Rigidbody>().velocity.magnitude > 3.5f && other.gameObject.name == "Ground" || gameObject.GetComponent<Rigidbody>().velocity.magnitude > 3.5f && other.gameObject.name == "Destroyable")
        {
          
            sfx.PlaySFX(2, 1.0f);
            
            StartCoroutine(gameObject.GetComponent<TriangleExplosion>().SplitMesh(true));
           
        }
    }

 }
