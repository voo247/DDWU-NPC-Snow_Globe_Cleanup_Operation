using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public bool isMuted = false;
    public AudioClip mainMenuMusic;
    public AudioClip gameMusic;
    public AudioClip badEndingMusic;
    public AudioClip happyEndingMusic;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.loop = true;
            audioSource.playOnAwake = false;

            if (PlayerPrefs.HasKey("Muted"))
            {
                isMuted = PlayerPrefs.GetInt("Muted") == 1;
            }

            SceneManager.sceneLoaded += OnSceneLoaded;

            UpdateAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
