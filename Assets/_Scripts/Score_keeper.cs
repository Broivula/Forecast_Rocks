using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score_keeper : MonoBehaviour {

    private Text score_text;
    private int currentScore;
    public AudioSource pointsAS;
    private Floating_Score floatingScore;
    private Text floating_text;
    public int counter = 0;
    public GameObject floating_score_text_O;

    public AudioClip[] scoreSFX;

    void Awake ()
    {
        pointsAS = GameObject.Find("Score_Audiosource").GetComponent<AudioSource>();
        score_text = GameObject.Find("Score_text").GetComponent<Text>();
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

    void Update ()
    {
        score_text.text = " " + currentScore;
    }

}
