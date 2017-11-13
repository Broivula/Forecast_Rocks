using UnityEngine;
using System.Collections;

public class Rotate_Catapult : MonoBehaviour {

    float clampedRotation;
    float rotSpeed = 5.0f;
    public bool isRotating = false;
    GameObject rotArm;
     Rotation_Arm arm;

    void Start ()
    {
        rotArm = GameObject.Find("catapult_arm");
        arm = rotArm.GetComponent<Rotation_Arm>();
    }

	void Update ()
    {
     
            isRotating = false;
	
	}


    void OnMouseDrag()
    {


        isRotating = true;
        clampedRotation = Mathf.Clamp(Mathf.Abs(gameObject.transform.localEulerAngles.y) + (Input.GetAxis("Mouse X") * -1 ), 155, 205);

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, clampedRotation, transform.localEulerAngles.z);

        //Debug.Log("asd " + clampedRotation);

            arm.orig_pos = rotArm.transform.rotation;
       
            //  releasePos = Input.mousePosition;

            // delta = originalPos - releasePos;
            //   power = Mathf.Abs(delta.y) / 2;
            //     power = Mathf.Clamp(power, 0, 100);

        

    }
}
