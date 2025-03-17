using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransTitle : MonoBehaviour
{
    public AudioSource clickSound;
    private bool isClicked = false;

    public void OnReturnButtonClick()
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
            SceneManager.LoadScene("Title");
        }
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Title");
    }
}
