using UnityEngine;

public class Dust : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color dustColor;
    private float eraseSpeed = 0.05f; // 문지를 때 줄어드는 알파 값

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        dustColor = spriteRenderer.color; // 원래 색상 저장
    }

    void OnMouseDrag() // 마우스 드래그 중이면 실행
    {
        EraseDust();
    }


    void EraseDust()
    {
        dustColor.a -= eraseSpeed; // 알파 값 줄이기
        spriteRenderer.color = dustColor;

        if (dustColor.a <= 0f)
        {
            Destroy(gameObject); // 먼지가 완전히 사라지면 삭제
            DustManager.Instance.RemoveDust(); // 먼지 개수 업데이트
        }
    }
}
