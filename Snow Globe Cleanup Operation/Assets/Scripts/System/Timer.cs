using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public GameObject[] stopObject = new GameObject[2];
    public Slider timeSlider;
    public float endTime = 100.0f;
    public float now;
    public string endScene;
    public static Timer Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        now = endTime;

        if (PlayerPrefs.HasKey("TimerValue"))
        {
            now = PlayerPrefs.GetFloat("TimerValue");
        }
        else
        {
            ResetTimer();
        }
    }

    void FixedUpdate()
    {
        if (stopObject[0].activeSelf == false && stopObject[1].activeSelf == false)
        {
            if (now >= 0.0f)
            {
                now -= Time.deltaTime;
                timeSlider.value = now / endTime;

                PlayerPrefs.SetFloat("TimerValue", now);
                PlayerPrefs.Save();
            }
            else
            {
                Timer.Instance?.ResetTimer();
                SceneManager.LoadScene("EndingStory_BAD");
            }
        }
    }

    public void ResetTimer()
    {
        now = endTime;
        PlayerPrefs.DeleteKey("TimerValue");
        PlayerPrefs.Save();
        timeSlider.value = 1.0f;
    }
}
