using System.Collections.Generic;
using UnityEngine;

public class ChangingClothesManager : MonoBehaviour
{
    public GameObject Success;
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
        Success.SetActive(true);
    }
}
