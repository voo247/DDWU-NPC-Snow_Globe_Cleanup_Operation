using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPopupManager : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private Button muteButton;
    [SerializeField] private Button unmuteButton;
    [SerializeField] private Button closeButton;
    public AudioSource clickSound;

    private void Awake()
    {
        muteButton.onClick.AddListener(OnMuteClicked);
        unmuteButton.onClick.AddListener(OnUnmuteClicked);
        closeButton.onClick.AddListener(ClosePopup);
    }

    private void OnMuteClicked()
    {
        AudioManager.Instance.MuteAudio();
    }

    private void OnUnmuteClicked()
    {
        AudioManager.Instance.UnmuteAudio();
    }

    public void ClosePopup()
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
        popup.SetActive(false);
    }
}