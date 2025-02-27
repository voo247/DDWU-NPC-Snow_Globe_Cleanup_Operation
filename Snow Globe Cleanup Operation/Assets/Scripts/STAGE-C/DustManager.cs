using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class DustManager : MonoBehaviour
{
    public GameObject[] dust = new GameObject[3];
    int count = 0;

    void Start()
    {
        for (int i = 0; i < 3; i++)
            if (dust[i] != null) count++;
    }

    void Update()
    {
        if (count > 0)
        {
            for (int i = 0; i < 3; i++)
                if (dust[i] != null) count++;
                else count--;
        }
    }
}