using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCan : MonoBehaviour, IDropHandler
{
    public Transform trashPosition; // ���� ���� ��ġ

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag; // �巡���� �� ��������
        if (droppedObject != null)
        {
            droppedObject.transform.SetParent(transform); // �θ� ������������ ����
            droppedObject.transform.position = trashPosition.position; // ������ ��ġ�� �̵�
        }
    }
}
