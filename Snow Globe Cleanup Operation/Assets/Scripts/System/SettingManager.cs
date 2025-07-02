using System.Collections;
using UnityEngine;

public class SettingButton : MonoBehaviour
{
    [SerializeField] private GameObject settingPopupPrefab; // 인스펙터에 연결
    private GameObject settingPopupInstance;

    public void OnClickSettingsButton()
    {
        if (settingPopupInstance == null)
        {
            settingPopupInstance = Instantiate(settingPopupPrefab);
            StartCoroutine(WaitAndSetStopObject());
        }
        else
        {
            settingPopupInstance.SetActive(true);
        }
    }

    IEnumerator WaitAndSetStopObject()
    {
        // Timer.Instance가 null이면 기다림
        while (Timer.Instance == null)
            yield return null;

        Timer.Instance.stopObject[1] = settingPopupInstance;
    }
}
