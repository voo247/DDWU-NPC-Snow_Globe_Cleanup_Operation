using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public GameObject[] stopObject = new GameObject[2]; //! ���� �� ����â (���� �ʼ�)
    public Slider timeSlider;       //! �����̴� ������Ʈ (���� �ʼ�)
    public float endTime = 60.0f;   //! ���� �ð� (�⺻�� 60, �ܺ� ���� ����)
    float now;                      //! �����̴��� ǥ���� ���� ���� �ð�
    public string endScene;         //! ���� ���� ��

    private void Awake()
    {
        // Ÿ�̸� ��ü�� �� ��ȯ �ÿ��� �ı����� �ʵ��� ����
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        now = endTime;

        // �̹� ����� Ÿ�̸� ���� ������ ����
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

                // Ÿ�̸� �� ����
                PlayerPrefs.SetFloat("TimerValue", now);
                PlayerPrefs.Save();
            }
            else
            {
                SceneManager.LoadScene("BADENDING");
            }
        }
    }
}
