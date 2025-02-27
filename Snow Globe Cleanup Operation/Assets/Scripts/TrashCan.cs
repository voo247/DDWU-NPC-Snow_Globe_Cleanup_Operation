using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCan : MonoBehaviour, IDropHandler
{
    public Transform trashPosition; // 옷을 놓을 위치

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag; // 드래그한 옷 가져오기
        if (droppedObject != null)
        {
            droppedObject.transform.SetParent(transform); // 부모를 쓰레기통으로 설정
            droppedObject.transform.position = trashPosition.position; // 지정된 위치로 이동
        }
    }
}
