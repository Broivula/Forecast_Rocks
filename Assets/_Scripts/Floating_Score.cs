using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Floating_Score : MonoBehaviour {

    Canvas gameCanvas;
    private float speed;
    private Vector3 direction;
    private float fadeTime;

    void Awake ()
    {
        gameCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();

    }


    public void SpawnPoints (int points)
    {
        
        GameObject textO = new GameObject();
        textO.transform.SetParent(gameCanvas.transform);
        Text spawnText = textO.AddComponent<Text>();
        spawnText.font = Resources.Load<Font>("Fonts/Sketch Match");
        spawnText.horizontalOverflow = HorizontalWrapMode.Overflow;
        spawnText.fontSize = 30;
        spawnText.rectTransform.anchorMin = new Vector2(0.5f, 1);
        spawnText.rectTransform.anchorMax = new Vector2(0.5f, 1);
        int randomNumberx = Random.Range(6, 7);
        int randomNumbery = Random.Range(3, 5);
        spawnText.rectTransform.transform.position = new Vector3((Screen.width/2) + (Screen.width/randomNumberx), (Screen.height/2) + (Screen.width/randomNumbery), 0);
       
        spawnText.text = " +" + points;

        
        StartCoroutine(WaitTime(spawnText.GetComponent<RectTransform>(),spawnText , textO));
       
    }

    IEnumerator WaitTime(RectTransform textO, Text spawnTextColor, GameObject text)

    {

        
        for (int i = 1; i < 70; i++)
        {
            float lerpTime = Mathf.PingPong(Time.time * 2, 2f);
            Color lerpedColor = Color.Lerp(Color.clear, Color.white, lerpTime);
            spawnTextColor.color = lerpedColor;

            textO.transform.position = new Vector3(textO.transform.position.x, textO.transform.position.y + (i/30), 0);   
            yield return new WaitForSeconds(0.01f);
           
        }
 //       yield return new WaitForSeconds(2);
        Debug.Log("loooppii");
        Destroy(text);
    }
}
