
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
        Debug.Log("���α׷� ����" +maxGauge);
    }

    public void SetGauge()
    {
        touchCnt++;
        gaugeBar.value = touchCnt;
        if (touchCnt < maxGauge)
        {
            Debug.Log("Ŭ�� Ƚ��: " + touchCnt);
        }
        else
        {
            Debug.Log("�̼� ����!" + touchCnt);
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
        yield return new WaitForSeconds(3f); // 3�� ���
        successPanel.SetActive(true); // �г� Ȱ��ȭ
    }
}
