using UnityEngine;
using UnityEngine.UI;

public class CameraZoom : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject SnowballImage;
    public GameObject buttonPanel;
    public Button SnowballButton;
    public float zoomSize = 2f;
    public float zoomSpeed = 2f;

    private Vector3 targetPosition;
    private float originalSize;
    private bool isZooming = false;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
        originalSize = mainCamera.orthographicSize;
        SnowballImage.gameObject.SetActive(false);
        buttonPanel.SetActive(false);
    }

    public void ZoomToTarget(Transform target)
    {
        Vector3 worldPosition = target.position;
        targetPosition = new Vector3(target.position.x, target.position.y, mainCamera.transform.position.z);
        SnowballImage.SetActive(true);
        isZooming = true;
    }

    private void Update()
    {
        if (isZooming)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * zoomSpeed);

            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, zoomSize, Time.deltaTime * zoomSpeed);

            if (Vector3.Distance(mainCamera.transform.position, targetPosition) < 0.1f && Mathf.Abs(mainCamera.orthographicSize - zoomSize) < 0.1f)
            {
                isZooming = false;
                buttonPanel.SetActive(true);
            }
        }
    }
}