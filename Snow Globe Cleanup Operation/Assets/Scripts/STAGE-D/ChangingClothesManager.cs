using System.Collections.Generic;
using UnityEngine;

public class ChangingClothesManager : MonoBehaviour
{
    public static ChangingClothesManager Instance;
    public List<DraggableClothes> clothesObjects;

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
            GameSuccess();
        }
    }

    public void GameSuccess()
    {
        // TO-DO
        // 여기 성공창 띄우고 미니게임 메인으로 돌아가는 부분 필요합니다!
    }
}
