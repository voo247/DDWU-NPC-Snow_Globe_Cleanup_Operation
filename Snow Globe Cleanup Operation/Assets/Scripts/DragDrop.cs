
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // �����ϰ� �����
        canvasGroup.blocksRaycasts = false; // �ٸ� UI ��� ���� �����ϵ��� ����
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / transform.lossyScale; // ���콺 ���� �̵�
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // �ٽ� ���� ���·� ����
        canvasGroup.blocksRaycasts = true;
    }
}
