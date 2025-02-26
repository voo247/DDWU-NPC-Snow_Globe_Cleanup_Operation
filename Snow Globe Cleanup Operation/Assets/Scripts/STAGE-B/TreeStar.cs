using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeStar : MonoBehaviour
{
    public GameObject targetObject;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ToggleObject()
    {
        targetObject.SetActive(targetObject.GetComponent<StarOnOff>().flag);
        targetObject.GetComponent<StarOnOff>().flag = !targetObject.GetComponent<StarOnOff>().flag;
        gameObject.SetActive(targetObject.GetComponent<StarOnOff>().flag);
    }
}
