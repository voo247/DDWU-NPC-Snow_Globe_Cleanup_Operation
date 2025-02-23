using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transHome : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("STAGE_E");
    }
}
