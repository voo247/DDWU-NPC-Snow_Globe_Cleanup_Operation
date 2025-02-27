using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public GameObject[] stopObject = new GameObject[2]; // 도움말 및 설정창
    public Slider timeSlider;  // 슬라이더 오브젝트
    public float endTime = 250.0f; // 제한 시간
    public float now;  // 현재 남은 시간
    public string endScene; // 게임 오버 씬
    public static Timer Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환 시 유지
        }
        else
        {
            Destroy(gameObject);  // 중복 방지
        }
    }

    private void Start()
    {
        now = endTime;

        // 이미 저장된 타이머 값이 있으면 복원
        if (PlayerPrefs.HasKey("TimerValue"))
        {
            now = PlayerPrefs.GetFloat("TimerValue");
        }
        else
        {
            ResetTimer();  // 저장된 값이 없으면 초기화
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

    public void ResetTimer()
    {
        now = endTime;  // 타이머를 초기 시간으로 되돌림
        PlayerPrefs.DeleteKey("TimerValue");  // 저장된 타이머 값 삭제
        PlayerPrefs.Save();
        timeSlider.value = 1.0f;  // 슬라이더도 초기 상태로 복원
    }
}
