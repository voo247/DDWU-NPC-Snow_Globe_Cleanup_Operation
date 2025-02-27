using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public GameObject popUp;
    public Button popUpButton;
    public Button continueButton;

    private void Start()
    {
        popUp.SetActive(false);

        popUpButton.onClick.AddListener(ShowPopUp);
        continueButton.onClick.AddListener(HidePopUp);
    }

    void ShowPopUp()
    {
        popUp.SetActive(true);
    }

    void HidePopUp()
    {
        popUp.SetActive(false);
    }
}

