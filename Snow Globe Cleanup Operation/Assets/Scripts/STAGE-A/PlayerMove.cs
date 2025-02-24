using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    float moveDistance = 1f;
    public LayerMask wallLayer;
    public LayerMask snowballLayer;
    public Button upButton;
    public Button downButton;
    public Button leftButton;
    public Button rightButton;

    public Vector2 exit = new Vector2(4.5f, -4.5f);

    Vector2 playerPosition;
    Vector2 snowballPosition;
    Collider2D lastMovedSnowball;
    int snowballMoveCnt;

    bool isSnowballMoved;

    private void Start()
    {
        upButton.onClick.AddListener(() => Move(Vector2.up));
        downButton.onClick.AddListener(() => Move(Vector2.down));
        leftButton.onClick.AddListener(() => Move(Vector2.left));
        rightButton.onClick.AddListener(() => Move(Vector2.right));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            Move(Vector2.up);
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            Move(Vector2.down);
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            Move(Vector2.left);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            Move(Vector2.right);
    }

    void Move(Vector2 direction)
    {
        Debug.Log(exit);
        Vector2 targetPosition = (Vector2)transform.position + direction * moveDistance;

        if (Physics2D.OverlapPoint(targetPosition, wallLayer))
            return;

        Collider2D snowball = Physics2D.OverlapPoint(targetPosition, snowballLayer);
        if (snowball != null)
        {
            Vector2 snowballTarget = targetPosition + direction * moveDistance;

            if (Physics2D.OverlapPoint(snowballTarget, wallLayer) || Physics2D.OverlapPoint(snowballTarget, snowballLayer))
                return;

            playerPosition = transform.position;
            snowballPosition = snowball.transform.position;
            lastMovedSnowball = snowball;
            isSnowballMoved = true;

            snowball.transform.position = snowballTarget;
            transform.position = targetPosition;

            snowballMoveCnt++;
            if (snowballMoveCnt % 3 == 0 && snowballMoveCnt <= 9)
            {
                snowball.transform.localScale *= 1.3f;
            }
            Debug.Log(snowball.transform.position);
            if (Vector2.Distance(snowball.transform.position, exit) <= 0.5f)
            {
                Debug.Log($"Snowball Position: " + snowball.transform.position + "Success Position: " + exit);
                Debug.Log("미션 성공");
            }
        }
        else
        {
            playerPosition = transform.position;
            transform.position = targetPosition;
            isSnowballMoved = false;
        }
    }

    public void Undo()
    {
        transform.position = playerPosition;

        if (isSnowballMoved && lastMovedSnowball != null)
        {
            lastMovedSnowball.transform.position = snowballPosition;

            if (snowballMoveCnt > 0)
            {
                if (snowballMoveCnt % 3 == 0 && snowballMoveCnt <= 9)
                {
                    lastMovedSnowball.transform.localScale /= 1.3f;
                }

                snowballMoveCnt--;
                isSnowballMoved = false;
            }
        }
    }
}
