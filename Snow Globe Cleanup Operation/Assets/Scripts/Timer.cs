using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public GameObject[] stopObject = new GameObject[2]; //! 도움말 및 설정창 (설정 필수)
    public Slider timeSlider;       //! 슬라이더 오브젝트 (설정 필수)
    public float endTime = 60.0f;   //! 제한 시간 (기본값 60, 외부 설정 가능)
    float now;                      //! 슬라이더에 표현될 현재 남은 시간
    public string endScene;         //! 게임 오버 씬

    private void Start()
    {
        now = endTime;
    }

    void FixedUpdate()
    {
        if (stopObject[0].activeSelf == false && stopObject[1].activeSelf == false)
        {
            if (now >= 0.0f)
            {
                now -= Time.deltaTime;
                timeSlider.value = now / endTime;
            }
            else
            {
                SceneManager.LoadScene(endScene);
            }
        }
    }
}
