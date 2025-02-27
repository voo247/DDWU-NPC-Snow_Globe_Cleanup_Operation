using TMPro; // TextMeshPro 네임스페이스 추가
using UnityEngine;
using UnityEngine.UI;

public class CameraZoom : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject SnowballImage;
    public GameObject buttonPanel;
    public Button SnowballButton;
    public TMP_Text text;
    public float zoomSize = 2f;
    public float zoomSpeed = 2f;
    public float blinkSpeed = 1f;

    private Vector3 targetPosition;
    private float originalSize;
    private bool isZooming = false;
    private bool isBlinking = false;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
        originalSize = mainCamera.orthographicSize;
        SnowballImage.SetActive(false);
        buttonPanel.SetActive(false);
        text.gameObject.SetActive(false); // 초기 비활성화
        StartBlinking();
    }

    public void ZoomToTarget(Transform target)
    {
        Vector3 worldPosition = target.position;
        targetPosition = new Vector3(worldPosition.x, worldPosition.y, mainCamera.transform.position.z);
        SnowballImage.SetActive(true);
        isBlinking = false;
        text.gameObject.SetActive(false);
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
