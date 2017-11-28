using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rocket_Launch : MonoBehaviour {

    private bool grounded = false;
    public int rocketTimer = 20;
    private Quaternion origRot;
    public Rotation_Arm rotArm;
    Score_keeper score;
    SFX_main sfx;
    GameObject canvasMain, manager;
    Text scoreText;
    GameObject powerSlider, restartButton;

    ParticleSystem p1, p2;


    private void Awake()
    {
        origRot = gameObject.transform.localRotation;
        manager = GameObject.Find("Game_Manager");
        rotArm = GameObject.Find("catapult_arm").GetComponent<Rotation_Arm>();
        canvasMain = GameObject.Find("Canvas");


        score = manager.GetComponent<Score_keeper>();
        sfx = manager.GetComponent<SFX_main>();
    }




    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground" && !grounded)
        {
            GameObject boulder = GameObject.Find("Boulder");
            scoreText = GameObject.Find("Score_text").GetComponent<Text>();
            powerSlider = GameObject.Find("Power_Slider");
            restartButton = GameObject.Find("Restart_Button");
            scoreText.enabled = false;
            powerSlider.gameObject.active = false;
            restartButton.active = false;


            if (boulder)
            {
                boulder.GetComponent<Rigidbody>().isKinematic = true;
            }
            p1 = GameObject.Find("P_1").GetComponent<ParticleSystem>();
            p2 = GameObject.Find("P_2").GetComponent<ParticleSystem>();

            p1.enableEmission = true;
            p2.enableEmission = true;
            grounded = true;
            //aloita rakettiäänet
            AudioSource.PlayClipAtPoint(sfx.sfx_collection[3], Camera.main.transform.position, 0.5f);
            StartCoroutine(LaunchRocket());
            rotArm.isBoulderDestroyed = false;
            rotArm.StopAllCoroutines();
            rotArm.StartCoroutine(rotArm.ChangeCamera(gameObject));
            rotArm.enabled = false;
            //anna pelaajalle x määrä pisteitä
            //kenttä suoritettu, vaihda kamera ja lopeta lvl
            score.AddPoints(1500, gameObject);

            
                
            }
    }

    IEnumerator LaunchRocket ()
    {
        Rigidbody rocketRB = gameObject.GetComponent<Rigidbody>();
        rocketRB.Sleep();
       // rocketRB.isKinematic = false;
        transform.rotation = origRot;
        for(int i = 0; i < rocketTimer;i++)
        {
            rocketRB.AddForce(Vector3.up * 2, ForceMode.Impulse);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        // level läpi! aloita loppujutut : 

        score.StartCoroutine(score.LevelCleared());
    }

}
