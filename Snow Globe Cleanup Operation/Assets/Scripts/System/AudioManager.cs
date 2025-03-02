<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
using System.Collections.Generic;
using UnityEngine;
=======
using UnityEngine;
using UnityEngine.SceneManagement;
>>>>>>> Stashed changes
=======
using UnityEngine;
using UnityEngine.SceneManagement;
>>>>>>> Stashed changes
=======
using UnityEngine;
using UnityEngine.SceneManagement;
>>>>>>> Stashed changes

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public bool isMuted = false;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    public AudioClip mainMenuMusic;
    public AudioClip gameMusic;
    public AudioClip badEndingMusic;
    public AudioClip happyEndingMusic;

    private AudioSource audioSource;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.loop = true;
            audioSource.playOnAwake = false;

<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
            if (PlayerPrefs.HasKey("Muted"))
            {
                isMuted = PlayerPrefs.GetInt("Muted") == 1;
            }
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======

            SceneManager.sceneLoaded += OnSceneLoaded;

>>>>>>> Stashed changes
=======

            SceneManager.sceneLoaded += OnSceneLoaded;

>>>>>>> Stashed changes
=======

            SceneManager.sceneLoaded += OnSceneLoaded;

>>>>>>> Stashed changes
            UpdateAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }

<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    private void UpdateAudio()
    {
        AudioListener.volume = isMuted ? 0f : 1f;
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);

        // 게임 씬인지 확인 (여러 개의 게임 씬을 관리)
        if (scene.name.StartsWith("STAGE"))  // 게임 씬이 여러 개일 경우
        {
            ChangeMusic(gameMusic);
        }
        else if (scene.name == "Main")
        {
            ChangeMusic(mainMenuMusic);
        }
        else if (scene.name == "EndingStory_BAD")
        {
            ChangeMusic(badEndingMusic);
        }
        else if (scene.name == "EndingStory_HAPPY")
        {
            ChangeMusic(happyEndingMusic);
        }
    }

    private void ChangeMusic(AudioClip newClip)
    {
        if (audioSource.clip == newClip && audioSource.isPlaying) return;

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();
        UpdateAudio();
    }

    private void UpdateAudio()
    {
        audioSource.volume = isMuted ? 0f : 1f;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
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
