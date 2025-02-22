using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float moveDistance = 1f;
    public LayerMask wallLayer;
    public LayerMask snowballLayer;

    public Vector2 successPosition = new Vector2(3.5f, -4.5f);

    Vector2 playerPosition;
    Vector2 snowballPosition;
    Collider2D lastMovedSnowball;
    int snowballMoveCnt;

    bool isSnowballMoved;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            Move(Vector2.up);
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            Move(Vector2.left);
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            Move(Vector2.down);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            Move(Vector2.right);
    }

    void Move(Vector2 direction)
    {
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
            if (Vector2.Distance(snowball.transform.position, successPosition) <= 0.5f)
            {
                Debug.Log("미션 성공");
            }
        }
        else // 눈덩이가 없는 경우 플레이어 이동
        {
            playerPosition = transform.position; // 이동 확정 후 저장
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
                snowballMoveCnt--;
        }
    }
}
