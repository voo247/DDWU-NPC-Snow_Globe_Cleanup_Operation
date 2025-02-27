using UnityEngine;
using UnityEngine.EventSystems;

public class ApplyClothes : MonoBehaviour, IDropHandler
{
    public Transform targetPosition; // 옷이 정렬될 위치

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null)
        {
            droppedObject.transform.SetParent(transform); // 부모를 눈사람으로 설정
            droppedObject.transform.position = targetPosition.position; // 정해진 위치로 이동

        }
    }
}
