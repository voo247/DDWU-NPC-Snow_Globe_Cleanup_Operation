using UnityEngine;
using UnityEngine.EventSystems;

public class ApplyClothes : MonoBehaviour, IDropHandler
{
    public Transform targetPosition; // ���� ���ĵ� ��ġ

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null)
        {
            droppedObject.transform.SetParent(transform); // �θ� ��������� ����
            droppedObject.transform.position = targetPosition.position; // ������ ��ġ�� �̵�

        }
    }
}
