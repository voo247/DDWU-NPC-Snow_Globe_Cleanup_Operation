using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class D_StartGame : MonoBehaviour
{
    public void SceneChange()
    {
        Debug.Log("��ư�� ���Ƚ��ϴ�! �� ���� �õ�");
        SceneManager.LoadScene("changeClothes");

    }
}