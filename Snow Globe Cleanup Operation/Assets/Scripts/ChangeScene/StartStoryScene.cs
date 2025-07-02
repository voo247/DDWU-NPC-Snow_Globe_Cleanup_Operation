using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startStoryScene : MonoBehaviour
{
    public AudioSource clickSound;
    private bool isClicked = false;

    public void StartStoryScene()
    {
        if (isClicked) return;

        isClicked = true;

        PlayerPrefs.DeleteKey("TimerValue");
        PlayerPrefs.DeleteKey("STAGEA");
        PlayerPrefs.DeleteKey("STAGEB");
        PlayerPrefs.DeleteKey("STAGEC");
        PlayerPrefs.DeleteKey("STAGED");
        PlayerPrefs.DeleteKey("STAGEE");

        PlayerPrefs.Save();

        if (clickSound != null)
        {
            clickSound.PlayOneShot(clickSound.clip);
            StartCoroutine(LoadSceneAfterDelay(clickSound.clip.length));
        }
        else
        {
            SceneManager.LoadScene("StartStory");
        }
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("StartStory"); 
    }
}
