using UnityEngine;

public class Dust : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color dustColor;
    private float eraseSpeed = 0.05f; // ������ �� �پ��� ���� ��

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        dustColor = spriteRenderer.color; // ���� ���� ����
    }

    void OnMouseDrag() // ���콺 �巡�� ���̸� ����
    {
        EraseDust();
    }


    void EraseDust()
    {
        dustColor.a -= eraseSpeed; // ���� �� ���̱�
        spriteRenderer.color = dustColor;

        if (dustColor.a <= 0f)
        {
            Destroy(gameObject); // ������ ������ ������� ����
            DustManager.Instance.RemoveDust(); // ���� ���� ������Ʈ
        }
    }
}
