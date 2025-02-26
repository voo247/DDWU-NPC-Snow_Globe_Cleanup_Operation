using UnityEngine;

public class InfoPopupManager : MonoBehaviour
{
    [SerializeField] private GameObject infoPopup;

    public void OpenInfoPopup()
    {
        infoPopup.SetActive(true);
    }

    public void CloseInfoPopup()
    {
        infoPopup.SetActive(false);
    }
}
