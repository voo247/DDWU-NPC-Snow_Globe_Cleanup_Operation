using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject stageA_Before;
    public GameObject stageA_After;
    public GameObject stageB_Before;
    public GameObject stageB_After;
    public GameObject stageC_Before;
    public GameObject stageC_After;
    public GameObject stageD_Before;
    public GameObject stageD_After;
    public GameObject stageE_Before;
    public GameObject stageE_After;

    void Start()
    {
        if(PlayerPrefs.GetInt("STAGEA", 0) == 1)
        {
            stageA_Before.SetActive(false);
            stageA_After.SetActive(true);
        }
        else
        {
            stageA_Before.SetActive(true);
            stageA_After.SetActive(false);
        }

        if(PlayerPrefs.GetInt("STAGEB", 0) == 1)
        {
            stageB_Before.SetActive(false);
            stageB_After.SetActive(true);
        }
        else
        {
            stageB_Before.SetActive(true);
            stageB_After.SetActive(false);
        }

        if(PlayerPrefs.GetInt("STAGEC", 0) == 1)
        {
            stageC_Before.SetActive(false);
            stageC_After.SetActive(true);
        }
        else
        {
            stageC_Before.SetActive(true);
            stageC_After.SetActive(false);
        }

        if(PlayerPrefs.GetInt("STAGED", 0) == 1)
        {
            stageD_Before.SetActive(false);
            stageD_After.SetActive(true);
        }
        else
        {
            stageD_Before.SetActive(true);
            stageD_After.SetActive(false);
        }

        if(PlayerPrefs.GetInt("STAGEE", 0) == 1)
        {
            stageE_Before.SetActive(false);
            stageE_After.SetActive(true);
        }
        else
        {
            stageE_Before.SetActive(true);
            stageE_After.SetActive(false);
        }
    }
}
