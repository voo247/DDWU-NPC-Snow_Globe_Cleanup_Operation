using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject buttonA; 
    public GameObject buttonB; 
    public GameObject buttonC;
    public GameObject buttonC2;
    public GameObject buttonC3;
    public GameObject buttonD; 
    public GameObject buttonE;
    public GameObject particleBox;
    public GameObject clothesBox;
    public GameObject tree;
    public GameObject snow;
    public GameObject stain;
    public Timer timer;

    void Start()
    {
        //PlayerPrefs.DeleteKey("STAGEA");
        //PlayerPrefs.DeleteKey("STAGEB");
        //PlayerPrefs.DeleteKey("STAGEC");
        //PlayerPrefs.DeleteKey("STAGED");
        //PlayerPrefs.DeleteKey("STAGEE");

        PlayerPrefs.Save();

        if (PlayerPrefs.GetInt("STAGEA", 0) == 1)
        {
            buttonA.SetActive(false);
            snow.SetActive(true);
        }
        if (PlayerPrefs.GetInt("STAGEB", 0) == 1)
        {
            buttonB.SetActive(false);
            tree.SetActive(true);
        }
        if (PlayerPrefs.GetInt("STAGEC", 0) == 1)
        {
            buttonC.SetActive(false); 
            buttonC2.SetActive(false);
            buttonC3.SetActive(false);
            stain.SetActive(true);
        }
        if (PlayerPrefs.GetInt("STAGED", 0) == 1)
        {
            buttonD.SetActive(false);
            clothesBox.SetActive(true);
        }
        if (PlayerPrefs.GetInt("STAGEE", 0) == 1)
        {
            buttonE.SetActive(false);
            particleBox.SetActive(true);
        }
        if (PlayerPrefs.GetInt("STAGEA", 0) == 1 && PlayerPrefs.GetInt("STAGEB", 0) == 1 && PlayerPrefs.GetInt("STAGEC", 0) == 1 && PlayerPrefs.GetInt("STAGED", 0) == 1 && PlayerPrefs.GetInt("STAGEE", 0) == 1 && PlayerPrefs.GetInt("STAGEE", 0) == 1 && timer.now > 0)
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
