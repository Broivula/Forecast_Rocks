using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level_Loader : MonoBehaviour {

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void RestartLevel ()
    {
        SceneManager.LoadScene(Application.loadedLevel);
    }
}
