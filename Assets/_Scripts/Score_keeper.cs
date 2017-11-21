using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Score_keeper : MonoBehaviour {

    private Text score_text;
    private int currentScore;
    public AudioSource pointsAS;
    private Floating_Score floatingScore;
    private Text floating_text;
    public int counter = 0;
    public GameObject floating_score_text_O;
    public int spawnNumber = 0;
    public List<RectTransform> spawnLocations;

    public AudioClip[] scoreSFX;

    public RectTransform[] spawn;

    void Awake ()
    {
        pointsAS = GameObject.Find("Score_Audiosource").GetComponent<AudioSource>();
        score_text = GameObject.Find("Score_text").GetComponent<Text>();
        spawnLocations = new List<RectTransform>();

        for(int i = 0; i < spawn.Length;i++)
        {
            spawnLocations.Add(spawn[i]);
          
        }

      
       
    }

    public void AddPoints (int points, GameObject destroyedObject)
    {
        counter = counter + 1;


        if (counter >= 5)
        {
            points = 5000;
            pointsAS.PlayOneShot(scoreSFX[1], 0.75f);
            GameObject bonus_scroll = Instantiate(floating_score_text_O, destroyedObject.transform.position, Quaternion.identity) as GameObject;
            Text bonus_text = bonus_scroll.GetComponent<Text>();
            bonus_text.text = " +" + points;
            bonus_text.fontSize = 55;
            currentScore = currentScore + 5000;
            counter = 0;
        }
        else
        {
            GameObject scroll = Instantiate(floating_score_text_O, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            scroll.GetComponent<Text>().text = " +" + points;

            currentScore = currentScore + points;
        }



        pointsAS.PlayOneShot(scoreSFX[0], 0.75f);
        pointsAS.pitch = pointsAS.pitch + 0.1f;


    }


    public RectTransform GetSpawnLocation ()
    {
        spawnNumber++;
        RectTransform spawn;
     
        if (spawnNumber >= 5)
        {
            spawnNumber = 0;
        }
        spawn = spawnLocations[spawnNumber];
        Debug.Log(spawnNumber);
        return spawn;
    }

    void Update ()
    {
        score_text.text = " " + currentScore;
    }

}
