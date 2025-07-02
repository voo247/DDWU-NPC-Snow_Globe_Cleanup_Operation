using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingClothesManager : MonoBehaviour
{
    public GameObject Success;
    public static ChangingClothesManager Instance;
    public List<DraggableClothes> clothesObjects;
    public ParticleSystem[] fireworks;
    public GameObject snowManSmile_Before;
    public GameObject snowManSmile_After;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CheckGameSuccess()
    {
        bool allSnapped = true;
        foreach (DraggableClothes clothes in clothesObjects)
        {
            Debug.Log(clothes.name + " isSnapped: " + clothes.isSnapped);
            if (!clothes.isSnapped)
            {
                allSnapped = false;
                break;
            }
        }
        
        if (allSnapped)
        {
            Debug.Log("성공");
            snowManSmile_Before.SetActive(false);
            snowManSmile_After.SetActive(true);
            TriggerFireworks();
        }
    }

    public void GameSuccess()
    {
        Success.SetActive(true);
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
        GameSuccess();
    }
}
