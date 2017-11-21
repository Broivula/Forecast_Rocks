using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class Score_floater : MonoBehaviour {

    private GameObject game_Canvas;
    private Text floating_Text;
    Score_keeper score;
    RectTransform spawnPos;

    void Awake ()
    {
        game_Canvas = GameObject.Find("Canvas");
        gameObject.transform.SetParent(game_Canvas.transform);
        score = GameObject.Find("Game_Manager").GetComponent<Score_keeper>();
        floating_Text = gameObject.GetComponent<Text>();


       
        spawnPos = score.GetSpawnLocation();
 
        Debug.Log(spawnPos + " kakka");

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
        //hae spawnpaikka



        gameObject.GetComponent<RectTransform>().localPosition = new Vector2(spawnPos.localPosition.x, spawnPos.localPosition.y);


        //     gameObject.GetComponent<RectTransform>().position = new Vector3((Screen.width / 5) + ((Screen.width / randomNumberx)), (Screen.height / 2) + (Screen.width / randomNumbery), 0);


        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }

}
