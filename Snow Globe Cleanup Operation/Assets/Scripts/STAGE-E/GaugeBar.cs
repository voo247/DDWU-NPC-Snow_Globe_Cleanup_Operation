using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GaugeBar : MonoBehaviour
{
    public Slider gaugeBar;
    public ParticleSystem[] fireworks;
    public GameObject successPanel;

    int touchCnt;
    int maxGauge;


    void Start()
    {
        gaugeBar.value = 0;
        touchCnt = 0;
        maxGauge = 100;

        if (fireworks != null && fireworks.Length > 0)
        {
            foreach (var firework in fireworks)
            {
                if (firework != null)
                    firework.Stop();
            }
        }
        Debug.Log("프로그램 시작" +maxGauge);
    }

    public void SetGauge()
    {
        touchCnt++;
        gaugeBar.value = touchCnt;
        if (touchCnt < maxGauge)
        {
            Debug.Log("클릭 횟수: " + touchCnt);
        }
        else
        {
            Debug.Log("미션 성공!" + touchCnt);
            TriggerFireworks();
        }
    }

    void TriggerFireworks()
    {
        for (int i = 0; i < fireworks.Length; i++)
        {  
            if (fireworks[i] != null && !fireworks[i].isPlaying)
            {
                fireworks[i].Play();
            }
        }
        StartCoroutine(ShowSuccessPanelAfterDelay());
    }

    IEnumerator ShowSuccessPanelAfterDelay()
    {
        yield return new WaitForSeconds(3f); // 3초 대기
        successPanel.SetActive(true); // 패널 활성화
    }
}
