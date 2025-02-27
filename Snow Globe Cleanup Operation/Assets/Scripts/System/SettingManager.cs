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
        }
        else
        {
            settingPopupInstance.SetActive(true);
        }
    }
}
