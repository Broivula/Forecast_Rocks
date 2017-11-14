using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score_floater : MonoBehaviour {

    private GameObject game_Canvas;
    private Text floating_Text;

    void Awake ()
    {
        game_Canvas = GameObject.Find("Canvas");
        gameObject.transform.SetParent(game_Canvas.transform);

        floating_Text = gameObject.GetComponent<Text>();
        StartCoroutine(DeathWait());
       


    }

    void Update ()
    {

        // gameObject.GetComponent<RectTransform>().transform.position = new Vector3(0, 0, 0);
  
        gameObject.transform.Translate(new Vector3(0, 5, 0) * 0.3f, Space.World);


    }


    IEnumerator DeathWait ()
    {
        floating_Text.enabled = false;
        yield return new WaitForEndOfFrame();
        floating_Text.enabled = true;
        int randomNumberx = Random.Range(1, 7);
        Debug.Log("rand " + randomNumberx);
        int randomNumbery = Random.Range(3, 7);


        gameObject.GetComponent<RectTransform>().localPosition = new Vector3((Screen.width / randomNumberx), (Screen.height / randomNumbery), 0);
        
   
           //     gameObject.GetComponent<RectTransform>().position = new Vector3((Screen.width / 5) + ((Screen.width / randomNumberx)), (Screen.height / 2) + (Screen.width / randomNumbery), 0);
               

        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }

}
