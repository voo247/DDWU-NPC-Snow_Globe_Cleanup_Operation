using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GaugeBar : MonoBehaviour
{
    public Slider gaugeBar;
    public ParticleSystem[] fireworks;

    int touchCnt;
    int maxGauge;
    public int playCnt = 5;
    public float interval = 5f;


    void Start()
    {
        gaugeBar.value = 0;
        touchCnt = 0;
        maxGauge = 10;

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
    }
}
