using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Timer gameTimer;
    
    void Start()
    {
        if (PlayerPrefs.GetInt("STAGEA", 0) == 1 && PlayerPrefs.GetInt("STAGEB", 0) == 1 && PlayerPrefs.GetInt("STAGEC", 0) == 1 && PlayerPrefs.GetInt("STAGED", 0) == 1 && PlayerPrefs.GetInt("STAGEE", 0) == 1 && PlayerPrefs.GetInt("STAGEE", 0) == 1 && gameTimer.now > 0)
            SceneManager.LoadScene("EndingStory_HAPPY");
    }

    public void LoadMiniGameA()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("STAGE_A");
    }

    public void LoadMiniGameB()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("STAGE_B");
    }

    public void LoadMiniGameC()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("STAGE_C");
    }

    public void LoadMiniGameD()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("STAGE_D");
    }

    public void LoadMiniGameE()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("STAGE_E");
    }
}
