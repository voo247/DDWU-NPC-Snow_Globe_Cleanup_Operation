using UnityEngine;

public class TextManager : MonoBehaviour
{
    public GameObject stageA;
    public GameObject stageB;
    public GameObject stageC1;
    public GameObject stageC2;
    public GameObject stageC3;
    public GameObject stageD;
    public GameObject stageE;

    void Start()
    {
        if(PlayerPrefs.GetInt("STAGEA", 0) == 1)
        {
            stageA.SetActive(false);
        }
        else
        {
            stageA.SetActive(true);
        }

        if (PlayerPrefs.GetInt("STAGEB", 0) == 1)
        {
            stageB.SetActive(false);
        }
        else
        {
            stageB.SetActive(true);
        }

        if (PlayerPrefs.GetInt("STAGEC", 0) == 1)
        {
            stageC1.SetActive(false);
            stageC2.SetActive(false);
            stageC3.SetActive(false);
        }
        else
        {
            stageC1.SetActive(true);
            stageC2.SetActive(true);
            stageC3.SetActive(true);
        }

        if (PlayerPrefs.GetInt("STAGED", 0) == 1)
        {
            stageD.SetActive(false);
        }
        else
        {
            stageD.SetActive(true);
        }

        if (PlayerPrefs.GetInt("STAGEE", 0) == 1)
        {
            stageE.SetActive(false);
        }
        else
        {
            stageE.SetActive(true);
        }
    }
}
