using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public GameObject[] stopObject = new GameObject[2]; // 도움말 및 설정창
    public Slider timeSlider;  // 슬라이더 오브젝트
    public float endTime = 60.0f; // 제한 시간
    public float now;  // 현재 남은 시간
    public string endScene; // 게임 오버 씬

    private void Awake()
    {
        // 타이머 객체가 씬 전환 시에도 파괴되지 않도록 설정
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        now = endTime;

        // 이미 저장된 타이머 값이 있으면 복원
        if (PlayerPrefs.HasKey("TimerValue"))
        {
            now = PlayerPrefs.GetFloat("TimerValue");
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

                // 타이머 값 저장
                PlayerPrefs.SetFloat("TimerValue", now);
                PlayerPrefs.Save();
            }
            else
            {
                SceneManager.LoadScene("EndingStory_BAD");
            }
        }
    }
}
