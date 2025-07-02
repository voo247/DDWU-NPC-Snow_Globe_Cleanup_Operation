
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GaugeBar : MonoBehaviour
{
    public Slider gaugeBar;
    public ParticleSystem[] fireworks;
    public GameObject successPanel;
    public GameObject snowMan_Before;
    public GameObject snowMan_After;
    public GameObject snowManSmile_Before;
    public GameObject snowManSmile_After;

    int touchCnt;
    int maxGauge;


    void Start()
    {
        gaugeBar.value = 0;
        touchCnt = 0;
        maxGauge = 70;

        if (PlayerPrefs.GetInt("STAGEC", 0) == 1)
        {
            snowMan_Before.SetActive(false);
            snowMan_After.SetActive(true);
        }
        else
        {
            snowMan_Before.SetActive(true);
            snowMan_After.SetActive(false);
        }

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
            if (PlayerPrefs.GetInt("STAGEC", 0) == 1)
            {
                snowMan_After.SetActive(false);
                snowManSmile_After.SetActive(true);
            }
            else
            {
                snowMan_Before.SetActive(false);
                snowManSmile_Before.SetActive(true);
            }
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
