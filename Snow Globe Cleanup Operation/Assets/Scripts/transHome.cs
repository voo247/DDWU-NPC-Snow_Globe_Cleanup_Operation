using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transHome : MonoBehaviour
{
    public AudioSource clickSound;
    private bool isClicked = false;

    public void OnSuccessButtonClick(string stage)
    {
        if (isClicked) return;

        isClicked = true;

        PlayerPrefs.SetInt(stage, 1);
        PlayerPrefs.Save();

        if (clickSound != null)
        {
            clickSound.PlayOneShot(clickSound.clip);
            StartCoroutine(LoadSceneAfterDelay(clickSound.clip.length));
        }
        else
        {
            SceneManager.LoadScene("MAIN");
        } 
    }

    public void OnHomeButtonClick()
    {
        if (isClicked) return;

        isClicked = true;

        if (clickSound != null)
        {
            clickSound.PlayOneShot(clickSound.clip);
            StartCoroutine(LoadSceneAfterDelay(clickSound.clip.length));
        }
        else
        {
            SceneManager.LoadScene("MAIN");
        }
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MAIN");
    }
}
