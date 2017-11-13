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
        
       // gameObject.transform.Translate(new Vector3(0, 5, 0) * 0.3f, Space.World);
     
    }


    IEnumerator DeathWait ()
    {
        /*
                int randomNumberx = Random.Range(1, 10);
                Debug.Log("rand " + randomNumberx);
                int randomNumbery = Random.Range(3, 5);

                gameObject.GetComponent<RectTransform>().position = new Vector3((Screen.width / 5) + ((Screen.width / randomNumberx)), (Screen.height / 2) + (Screen.width / randomNumbery), 0);
                */
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }

}
