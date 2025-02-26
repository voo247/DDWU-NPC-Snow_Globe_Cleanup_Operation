using UnityEngine;
using UnityEngine.UI;

public class TreeObject : MonoBehaviour
{
    public GameObject targetObject;
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ToggleObject()
    {
        targetObject.SetActive(true);
        targetObject.GetComponent<OnOff>().targetObject[0].SetActive(false);
        targetObject.GetComponent<OnOff>().targetObject[1].SetActive(false);
        targetObject.GetComponent<OnOff>().targetObject[2].SetActive(false);
        targetObject.GetComponent<OnOff>().flag = 0;
    }
}
