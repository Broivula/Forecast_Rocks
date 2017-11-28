using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Score_keeper : MonoBehaviour {

    private Text score_text;
    private Text end_scoretext;
    private int currentScore;
    public AudioSource pointsAS;
    private Floating_Score floatingScore;
    private Text floating_text;
    private Text unspent1, unspent2, clear;
    public int counter = 0;
    public GameObject floating_score_text_O;
    public int spawnNumber = 0;
    public List<RectTransform> spawnLocations;
    private int kerroin = 50;
    public AudioClip[] scoreSFX;
    public List<int> pointsNeeded;
    public RectTransform[] spawn;

    void Awake ()
    {
        pointsAS = GameObject.Find("Score_Audiosource").GetComponent<AudioSource>();
        score_text = GameObject.Find("Score_text").GetComponent<Text>();
        spawnLocations = new List<RectTransform>();

        unspent1 = GameObject.Find("Unspent_text_1").GetComponent<Text>();
        unspent2 = GameObject.Find("Unspent_text_2").GetComponent<Text>();
        
        for (int i = 0; i < spawn.Length;i++)
        {
            spawnLocations.Add(spawn[i]);
          
        }

      
       
    }


    void Update()
    {
        score_text.text = " " + currentScore;
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
        pointsAS.pitch = pointsAS.pitch + 0.05f;


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

        return spawn;
    }

    public IEnumerator LevelCleared()
    {
        end_scoretext = GameObject.Find("Endscore_number").GetComponent<Text>();
        Animator winAnim = GameObject.Find("Winning_Panel").GetComponent<Animator>();
        Rotation_Arm arm = GameObject.Find("catapult_arm").GetComponent<Rotation_Arm>();
        


        winAnim.SetTrigger("Winning");
        int scorenumber = 0;

        for (int i = 0; i < currentScore; i++)
        {
            if (currentScore > 12500)
                kerroin = 65;
            scorenumber = i * kerroin;
            end_scoretext.text = " " + scorenumber;
            if(scorenumber > currentScore)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }

        end_scoretext.text = "" + currentScore;

        if(arm.numberOfThrows == 1)
        {
            scorenumber = currentScore;
            yield return new WaitForSeconds(1f);
            AudioSource.PlayClipAtPoint(scoreSFX[2], Camera.main.transform.position, 0.8f);
            unspent1.enabled = true;
            //toista ääniefekti
       

            yield return new WaitForSeconds(1f);
            AudioSource.PlayClipAtPoint(scoreSFX[2], Camera.main.transform.position, 0.8f);
            unspent2.enabled = true;
            //toista ääniefekti
           
            scorenumber = currentScore;
            currentScore = currentScore + 10000;

            for(int i = 0; i < currentScore; i++)
            {
                scorenumber = scorenumber + (i + 2);
                end_scoretext.text = " " + scorenumber;
                yield return new WaitForEndOfFrame();
                if (scorenumber > currentScore)
                    break;
            }

            Debug.Log("Kaksi pallo jäljel");
        }
        else if(arm.numberOfThrows == 2)
        {
            yield return new WaitForSeconds(0.5f);
            AudioSource.PlayClipAtPoint(scoreSFX[2], Camera.main.transform.position, 0.8f);
            unspent1.enabled = true;
            //toista ääniefekti
           
            scorenumber = currentScore;
            currentScore = currentScore + 5000;


            for (int i = 0; i < currentScore; i++)
            {
                Debug.Log("kaksi palloo kertaa");
                scorenumber = scorenumber + (i + 2);
                end_scoretext.text = " " + scorenumber;
                yield return new WaitForEndOfFrame();
                if (scorenumber > currentScore)
                    break;
            }

            Debug.Log("Yksi pallo jäljel");
        }
        clear = GameObject.Find("Clear_text").GetComponent<Text>();
        if(!clear)
        {
            clear = GameObject.Find("Clear_text").GetComponent<Text>();
        }
        end_scoretext.text = " " + currentScore;

        yield return new WaitForSeconds(1.5f);
        if (currentScore >= pointsNeeded[SceneManager.sceneCount])
        {
            //ääniefekti voitosta
            AudioSource.PlayClipAtPoint(scoreSFX[3], Camera.main.transform.position, 0.5f);
            clear.enabled = true;
        }


       
    }

 

}
