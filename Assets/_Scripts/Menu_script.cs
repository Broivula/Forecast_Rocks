using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu_script : MonoBehaviour {

    private Animator menu_Anim;
    private Animator world_1_Anim;

    void Start ()
    {
        menu_Anim = GameObject.Find("World_Select_button").GetComponent<Animator>();
        world_1_Anim = GameObject.Find("World_1_Levels").GetComponent<Animator>();
    }

    public void WorldSelect(int menuAction)
    {

        switch (menuAction)
        {
            case 0:
                   //aktivoi world selectin ja tuo kentät näkyviin
                   menu_Anim.SetTrigger("World_Select");
                   Debug.Log("testi alkuvalikko");
                   break;

            case 1:
                //aktivoi ensimmäiset maailmat
                
                Debug.Log("testi ekat maailmat");
                menu_Anim.SetTrigger("World_Selected");
                world_1_Anim.SetTrigger("World_1_select");
                break;

            case 2:
                //aktivoi toiset maailmat

                Debug.Log("testi toiset maailmat");
                menu_Anim.SetTrigger("World_Selected");
                break;

            case 3:
                //aktivoi kolmannet maailmat

                Debug.Log("testi kolmannet maailmat");
                menu_Anim.SetTrigger("World_Selected");
                break;

            default:
                //palauttaa maailmavalikon alkuun
                break;


        }
    }

    public void WorldOneSelect(int level)
    {
        switch (level)
        {
            case 0:
                //palaa päävalikkoon
                break;

            case 1:
                //lataa level 1
                break;

            case 2:
                //lataa level 2
                break;

            case 3:
                //lataa level 3
                break;

            default:
                //palauttaa maailmavalikon alkuun
                break;
        }
    }

}
