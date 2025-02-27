using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class B_Click : MonoBehaviour
{
    public GameObject Comb, Star, StarD, Box;
    public GameObject[] SB = new GameObject[2];
    public GameObject[] SBD = new GameObject[6];
    public GameObject Tree1, Tree2;
    int[] count = { 0, 0 };

    void Start()
    {
        Box.SetActive(true);
        Comb.SetActive(true);

        SB[0].SetActive(false);
        Star.SetActive(false);
        SB[1].SetActive(false);

        StarD.SetActive(false);
        for (int i = 0; i < 6; i++)
            SBD[i].SetActive(false);

        Tree1.SetActive(true);
        Tree2.SetActive(false);
    }

    public void OpenBox(GameObject box)
    {
        EventSystem.current.currentSelectedGameObject.SetActive(false);
        box.SetActive(false);

        Tree1.SetActive(false);
        Tree2.SetActive(true);

        SB[0].SetActive(true);
        Star.SetActive(true);
        SB[1].SetActive(true);
    }

    public void ToggleOnce(GameObject target)
    {
        GameObject self = EventSystem.current.currentSelectedGameObject;
        self.SetActive(false);
        target.SetActive(true);
    }

    public void Toggle(int index)
    {
        int add = (index == 0 ? 0 : 3);
        GameObject self = EventSystem.current.currentSelectedGameObject;
        if (count[index] < 2)
        {
            SBD[count[index] + add].SetActive(true);
            count[index]++;
        }
        else
        {
            SBD[count[index] + add].SetActive(true);
            self.SetActive(false);
        }
    }

    public void Close(int index)
    {
        int add = (index == 0 ? 0 : 3);
        SB[index].SetActive(true);
        for (int i = 0; i < 3; i++)
            SBD[i + add].SetActive(false);
        count[index] = 0;
    }
}