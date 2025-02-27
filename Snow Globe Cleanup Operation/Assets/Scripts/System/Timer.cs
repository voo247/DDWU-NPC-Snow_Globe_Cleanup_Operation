using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public GameObject[] stopObject = new GameObject[2]; // ���� �� ����â
    public Slider timeSlider;  // �����̴� ������Ʈ
    public float endTime = 250.0f; // ���� �ð�
    public float now;  // ���� ���� �ð�
    public string endScene; // ���� ���� ��
    public static Timer Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // �� ��ȯ �� ����
        }
        else
        {
            Destroy(gameObject);  // �ߺ� ����
        }
    }

    private void Start()
    {
        now = endTime;

        // �̹� ����� Ÿ�̸� ���� ������ ����
        if (PlayerPrefs.HasKey("TimerValue"))
        {
            now = PlayerPrefs.GetFloat("TimerValue");
        }
        else
        {
            ResetTimer();  // ����� ���� ������ �ʱ�ȭ
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

                // Ÿ�̸� �� ����
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
        now = endTime;  // Ÿ�̸Ӹ� �ʱ� �ð����� �ǵ���
        PlayerPrefs.DeleteKey("TimerValue");  // ����� Ÿ�̸� �� ����
        PlayerPrefs.Save();
        timeSlider.value = 1.0f;  // �����̴��� �ʱ� ���·� ����
    }
}
