using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Rotation_Arm : MonoBehaviour
{

    //arvoja liittyen heittoon
    float rotSpeed = 2.5f;
    float releaseLimit = 20.0f;

    //arvoja liittyen pisteisiin
    public int numberOfThrows = 0;
    Score_keeper scoreKeeper;
 //   bool isTouching = false;
    GameObject catapult_arm;
    Animator catapult_arm_anim;

    //arvoja liittyen kiveen
    Camera boulderCam;
    public GameObject boulder;
    public Quaternion orig_pos;
    Rotate_Catapult rotateCatapult;
    Transform spawnPoint;
    bool moving, fireReady;
    float power;
    public GameObject cloneBoulder;
    MeshRenderer fakeBoulder;

    Vector3 originalPos, releasePos, delta;
    float clampedRotation, releasePower;
    public bool isBoulderDestroyed;
    Vector3 originalCameraTransform;
    Slider powerSlider;
    int randomNumber;

    //sfx
    SFX_main sfx;

    Text text;      //debug text

    void Awake ()
    {
        catapult_arm = gameObject;                                                                              //main gameobject itself
        spawnPoint = GameObject.Find("Boulder_Spawn_Point").GetComponent<Transform>();                          //transform for the spawn point of the rocks
        text = GameObject.Find("Text").GetComponent<Text>();                                                    //debug text
        fireReady = false;                                                                                      
        rotateCatapult = GameObject.Find("Catapult").GetComponent<Rotate_Catapult>();
        boulderCam = GameObject.Find("Boulder_Cam").GetComponent<Camera>();
        isBoulderDestroyed = true;
        originalCameraTransform = Camera.main.transform.position;
        powerSlider = GameObject.Find("Power_Slider").GetComponent<Slider>();
        orig_pos = catapult_arm.transform.rotation;
        sfx = GameObject.Find("Game_Manager").GetComponent<SFX_main>();
        fakeBoulder = GameObject.Find("Boulder_fake").GetComponent<MeshRenderer>();
        catapult_arm_anim = catapult_arm.GetComponent<Animator>();
        scoreKeeper = GameObject.Find("Game_Manager").GetComponent<Score_keeper>();

    }



    void Update()
    {
        powerSlider.value = releasePower;

 //       Debug.Log(clampedRotation);
        if (releasePower > releaseLimit)
        {
            fireReady = true;
        }
        else if(releasePower < releaseLimit)
        {
            fireReady = false;
        }
        

        if (Input.GetMouseButtonDown(0))
        {
            
            originalPos = Input.mousePosition;

        }

        if (Input.GetMouseButton(0))
        {
        
            moving = true;
            
        }

        if(!moving)
        {
           
            power = 0;
        }

        //pallo luodaan
        if(Input.GetMouseButtonUp(0) && fireReady)
        {
          
           StartCoroutine(WaitTime());
           LaunchBoulder();

        }

        if(!isBoulderDestroyed)
        {
            cloneBoulder = GameObject.Find("Boulder");
        //    Camera.main.enabled = false;
            boulderCam.enabled = true;  
            boulderCam.transform.LookAt(cloneBoulder.transform);
        }
        else if(isBoulderDestroyed)
        {
            fakeBoulder.enabled = true;
            boulderCam.enabled = false;
            GameObject.FindWithTag("MainCamera").GetComponent<Camera>().enabled = true;
        }
    


      //        Debug.Log("asd " + fireReady);
      //  Debug.Log("release power " + releasePower);

    }

        void LaunchBoulder ()
    {
        randomNumber = Random.Range(0, 6);
        Debug.Log("number " + randomNumber);
        Camera.main.transform.position = originalCameraTransform;
        moving = false;
        if (!rotateCatapult.isRotating)                             //varmistus vain siitä, ettei katapultti ammu kun sitä käännetään
        {
            numberOfThrows = numberOfThrows + 1;
            scoreKeeper.pointsAS.pitch = 1.0f;
            scoreKeeper.counter = 0;
            fakeBoulder.enabled = false;                                                               //katapultissa olevan feikkikiven näkyvyys
            sfx.PlaySFX(1, 0.6f);
            catapult_arm_anim.SetTrigger("Catapult_launch");
            Camera.main.enabled = false;   
            if(!cloneBoulder)
            cloneBoulder = Instantiate(boulder, spawnPoint.position, Quaternion.identity) as GameObject;
            cloneBoulder.gameObject.name = "Boulder";
            cloneBoulder.GetComponent<Projectil_Power>().AddPhysicsToBoulder(releasePower / 4);
            
            //tee sfx kiven laukaisusta

            releasePower = 0;
        }

        isBoulderDestroyed = false;
    }

    void CameraMovement ()
    {
        //  Camera.main.enabled = false;
        if (!isBoulderDestroyed && fireReady)
        {
         
            fireReady = false;
        }
        else
        {
            boulderCam.enabled = false;
         
        }
    }

    void OnMouseDrag()
    {

        if (moving)
            {

                       
                    clampedRotation = Mathf.Clamp(Mathf.Abs(transform.localEulerAngles.x) + ((Input.GetAxis("Mouse Y") * -1) * rotSpeed), 0, 90);
                      
                    transform.localEulerAngles = new Vector3(clampedRotation, transform.localEulerAngles.y, transform.localEulerAngles.z);
                 

                    Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, (-40f + ((clampedRotation / 20) * -1)));
                    releasePower = clampedRotation;
                    // tee sfx katapultin latauksesta
                    // sfx.PlaySFX(0);
                    // Debug.Log(clampedRotation + " rot");

            }
    
    }

    IEnumerator WaitTime ()
    {
        yield return new WaitForSeconds(1.2f);
     
        catapult_arm.transform.rotation = orig_pos;
        clampedRotation = 0;
        power = 0;
    }






    /*
    if (Input.GetMouseButton(0))
    {
        float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

        //   transform.RotateAround(Vector3.up, -rotX);
        transform.RotateAround(Vector3.right, rotY);
        releasePos = Input.mousePosition;
    }

    if (Input.GetMouseButtonUp(0))
    {
        catapult_arm.transform.rotation = orig_pos;
        power = 0;
    }

    delta = originalPos - releasePos;



    power = Mathf.Abs(delta.y);
    Debug.Log("delta " + delta + " ja  power" + power);


#if UNITY_ANDROID

    /*
    int numberOfTouches = Input.touchCount;

    if (numberOfTouches > 0)
    {


        Touch touch = Input.GetTouch(0);

        text.text = "kolmas";

        if (numberOfTouches > 0 && numberOfTouches < 2 && !isTouching)
        {
            isTouching = true;
            float touchRotY = touch.deltaPosition.y * rotSpeed * Mathf.Deg2Rad;
             text.text = "toka";

            if (touch.phase == TouchPhase.Began)
            {
                originalPos = Input.touches[0].position;

            }

            if (touch.phase == TouchPhase.Moved)
            {
                  releasePos = Input.touches[0].position;
                // transform.Rotate(gameObject.transform.rotation.x - touchRotY, 0, 0);
               // gameObject.transform.RotateAround(Vector3.right, touch.deltaPosition.y);
                  gameObject.transform.Rotate(touch.deltaPosition.y * rotationRate, 0, 0, Space.World);
                text.text = "eka";
            }


        }
    }

    */


}
