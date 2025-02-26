using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class D_StartGame : MonoBehaviour
{
    public void SceneChange()
    {
        Debug.Log("버튼이 눌렸습니다! 씬 변경 시도");
        SceneManager.LoadScene("changeClothes");

    }
}