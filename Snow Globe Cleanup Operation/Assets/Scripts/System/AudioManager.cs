using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public bool isMuted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (PlayerPrefs.HasKey("Muted"))
            {
                isMuted = PlayerPrefs.GetInt("Muted") == 1;
            }
            UpdateAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void UpdateAudio()
    {
        AudioListener.volume = isMuted ? 0f : 1f;
    }

    public void MuteAudio()
    {
        isMuted = true;
        PlayerPrefs.SetInt("Muted", 1);
        UpdateAudio();
    }

    public void UnmuteAudio()
    {
        isMuted = false;
        PlayerPrefs.SetInt("Muted", 0);
        UpdateAudio();
    }
}
