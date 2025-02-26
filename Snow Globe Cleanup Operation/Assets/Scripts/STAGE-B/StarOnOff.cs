using UnityEngine;
using UnityEngine.UI;

public class StarOnOff : MonoBehaviour
{
    public GameObject targetObject;
    public bool flag = false;

    void Start()
    {
        gameObject.SetActive(true);
        if (targetObject != null)
            targetObject.SetActive(false);
    }

    public void ToggleObject()
    {
        gameObject.SetActive(flag);  //! false
        flag = !flag;
        targetObject.SetActive(flag);  //! true
    }
}
