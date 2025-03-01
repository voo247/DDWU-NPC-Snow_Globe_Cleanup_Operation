using UnityEngine;
using UnityEngine.SceneManagement;

public class startStoryScene : MonoBehaviour
{
    public void StartStoryScene()
    {
        PlayerPrefs.DeleteKey("TimerValue");
        PlayerPrefs.DeleteKey("STAGEA");
        PlayerPrefs.DeleteKey("STAGEB");
        PlayerPrefs.DeleteKey("STAGEC");
        PlayerPrefs.DeleteKey("STAGED");
        PlayerPrefs.DeleteKey("STAGEE");

        PlayerPrefs.Save();
        SceneManager.LoadScene("StartStory");
    }
}
