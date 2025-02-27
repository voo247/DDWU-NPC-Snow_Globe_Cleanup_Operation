using UnityEngine;
using UnityEngine.SceneManagement;

public class transHome : MonoBehaviour
{
    public void OnSuccessButtonClick(string stage)
    {
        PlayerPrefs.SetInt(stage, 1); 
        PlayerPrefs.Save();
        SceneManager.LoadScene("MAIN"); 
    }

    public void OnHomeButtonClick()
    {
        SceneManager.LoadScene("MAIN");
    }
}
