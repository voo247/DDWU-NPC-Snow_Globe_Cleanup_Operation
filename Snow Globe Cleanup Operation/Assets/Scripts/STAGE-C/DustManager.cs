using UnityEngine;
using UnityEngine.UI;

public class DustManager : MonoBehaviour
{
    public static DustManager Instance;
    public GameObject dustPrefab;
    public int dustCount = 10;
    public Text dustCounterText;

    private int remainingDust;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        remainingDust = dustCount;
        UpdateUI();
        SpawnDust();
    }

    void SpawnDust()
    {
        for (int i = 0; i < dustCount; i++)
        {
            Vector2 randomPos = new Vector2(Random.Range(-7f, 7f), Random.Range(-4f, 4f));
            Instantiate(dustPrefab, randomPos, Quaternion.identity);
        }
    }

    public void RemoveDust()
    {
        remainingDust--;
        UpdateUI();

        if (remainingDust <= 0)
        {
            Debug.Log("모든 먼지를 제거했습니다!");
            dustCounterText.text = "청소 완료!";
        }
    }

    void UpdateUI()
    {
        dustCounterText.text = "남은 먼지: " + remainingDust;
    }
}