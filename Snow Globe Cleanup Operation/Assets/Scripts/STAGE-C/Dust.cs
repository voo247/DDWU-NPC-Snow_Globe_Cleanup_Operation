using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dust : MonoBehaviour
{
    Image img;
    public GameObject tnrjs;

    void Start()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        if (tnrjs != null && IsTouching(tnrjs))
        {
            EraseDust();
        }
    }

    bool IsTouching(GameObject obj)
    {
        RectTransform dustRect = GetComponent<RectTransform>();
        RectTransform handkerchiefRect = obj.GetComponent<RectTransform>();

        return Vector2.Distance(dustRect.anchoredPosition, handkerchiefRect.anchoredPosition) < 250f;
    }

    void EraseDust()
    {
        if (img.color.a > 0.1f)
        {
            Color c = img.color;
            c.a -= 0.007f;
            img.color = c;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}