using UnityEngine;
using UnityEngine.SceneManagement;

public class transHome : MonoBehaviour
{
    public void OnSuccessButtonClick(string stage)
    {
        PlayerPrefs.SetInt(stage, 1); // 성공 상태 저장
        PlayerPrefs.Save();
        SceneManager.LoadScene("MAIN"); // 메인 화면으로 이동
    }
}
