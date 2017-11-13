using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level_Loader : MonoBehaviour {


    public void RestartLevel ()
    {
        SceneManager.LoadScene(Application.loadedLevel);
    }
}
