using UnityEngine;
using UnityEngine.UI;

public class ToggleVisibility : MonoBehaviour
{
    public GameObject targetObject;     //! 눌렀을 때 열릴 창(지정 필수)
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
