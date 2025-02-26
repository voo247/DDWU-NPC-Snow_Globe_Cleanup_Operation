using UnityEngine;
using UnityEngine.UI;

public class OnOff : MonoBehaviour
{
    public GameObject[] targetObject = new GameObject[3];
    public int flag = 0;

    void Start()
    {
        gameObject.SetActive(true);
        if (targetObject[2] != null)
        {
            targetObject[0].SetActive(false);
            targetObject[1].SetActive(false);
            targetObject[2].SetActive(false);
        }
    }

    public void ToggleObject()
    {
        if (flag < 2)
        {
            targetObject[flag].SetActive(true);
            gameObject.SetActive(true);
            flag++;
        }
        else
        {
            targetObject[flag].SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
