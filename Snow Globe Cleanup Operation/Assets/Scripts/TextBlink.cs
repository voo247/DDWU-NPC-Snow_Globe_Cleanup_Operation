using TMPro;
using UnityEngine;

public class TextBlink : MonoBehaviour
{
    public TMP_Text text;
    public float blinkSpeed = 1f;
    private bool isBlinking = false;

    void Start()
    {
        StartBlinking();
    }

    private void StartBlinking()
    {
        isBlinking = true;
        text.gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        if (isBlinking)
        {
            float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1.0f);
            Color textColor = text.color;
            textColor.a = alpha;
            text.color = textColor;
        }
    }
}
