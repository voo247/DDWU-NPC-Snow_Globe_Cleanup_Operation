using UnityEngine;
using UnityEngine.UI;

public class ToggleVisibility : MonoBehaviour
{
    public GameObject targetObject;     //! ������ �� ���� â(���� �ʼ�)
    private bool flag = false;

    void Start()
    {
        gameObject.SetActive(true);
        if (targetObject != null)
            targetObject.SetActive(flag);
    }

    public void ToggleObject()
    {
        flag = !flag;
        targetObject.SetActive(flag);
    }
}
