using UnityEngine;
using UnityEngine.SceneManagement;

public class TransTitle : MonoBehaviour
{
    public void OnReturnButtonClick()
    {
        SceneManager.LoadScene("Title");
    }
}
