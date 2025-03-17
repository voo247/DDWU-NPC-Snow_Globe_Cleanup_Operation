using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

//all dust manager
public class DustManager : MonoBehaviour
{
    public GameObject Success;
    public GameObject[] dust = new GameObject[3];
    int[] count = { 1, 1, 1 };

    void Update()
    {
        if ((count[0] > 0) || (count[1] > 0) || (count[2] > 0))
        {
            for (int i = 0; i < 3; i++)
                if (dust[i] != null) count[i] = 1;
                else count[i] = 0;
        }
        else if ((count[0] == 0) && (count[1] == 0) && (count[2] == 0))
        {
            count[0]--;
            count[1]--;
            count[2]--;
            Success.SetActive(true);
        }
    }
}