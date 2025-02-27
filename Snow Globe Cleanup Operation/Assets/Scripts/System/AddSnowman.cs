using UnityEditor.VisionOS;
using UnityEngine;

public class AddSnowman : MonoBehaviour
{
    public GameObject snowMan_Before;
    public GameObject snowMan_After;

    private void Start()
    {
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
    }
}
