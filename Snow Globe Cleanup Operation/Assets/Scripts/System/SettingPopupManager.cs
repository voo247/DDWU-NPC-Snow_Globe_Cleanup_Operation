using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPopupManager : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private Button muteButton;
    [SerializeField] private Button unmuteButton;
    [SerializeField] private Button closeButton;

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
        popup.SetActive(false);
    }
}