using UnityEngine;
using UnityEngine.EventSystems;

public enum ClothesType { Old, New }

public class DraggableClothes : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ClothesType clothesType;
    public RectTransform snapTarget;
    public float snapThreshold = 50f;

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    
    public bool isSnapped = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    void Start()
    {
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
        isSnapped = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (snapTarget != null)
        {
            float distance = Vector2.Distance(rectTransform.anchoredPosition, snapTarget.anchoredPosition);
            if (distance <= snapThreshold)
            {
                rectTransform.anchoredPosition = snapTarget.anchoredPosition;
                isSnapped = true;
            }
            else
            {
                rectTransform.anchoredPosition = originalPosition;
            }
        }
        else
        {
            rectTransform.anchoredPosition = originalPosition;
        }

        canvasGroup.blocksRaycasts = true;

        ChangingClothesManager.Instance.CheckGameSuccess();
    }
}
