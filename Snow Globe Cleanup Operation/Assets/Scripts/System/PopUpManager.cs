using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public GameObject popUp;
    public Button popUpButton;
    public Button continueButton;
    public AudioSource clickSound;

    private void Start()
    {
        popUpButton.onClick.AddListener(ShowPopUp);
        continueButton.onClick.AddListener(HidePopUp);
    }

    void ShowPopUp()
    {
        popUp.SetActive(true);
    }

    void HidePopUp()
    {
        if (clickSound != null)
        {
            clickSound.PlayOneShot(clickSound.clip);
            StartCoroutine(LoadSceneAfterDelay(clickSound.clip.length));
        }
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        popUp.SetActive(false);
    }
}

