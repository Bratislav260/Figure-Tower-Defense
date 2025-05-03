using UnityEngine;

/// <summary>
/// Класс движении юнита
/// </summary>
public class UnitMovement : MonoBehaviour
{
    private float moveSpeed = 2f;
    private new Rigidbody2D rigidbody;
    public int pathPointIndex { get; private set; } = 0;
    private bool isMoving;
    private Back back;
    private Front front;
    private Transform collirder;

    private Transform currentPathPoint;
    private Vector2 currentDirection;

    public void Initialize(Rigidbody2D rigidbody, float moveSpeed, int pathPointIndex)
    {
        this.rigidbody = rigidbody;
        this.moveSpeed = moveSpeed;
        this.pathPointIndex = pathPointIndex;

        if (transform.childCount > 0)
        {
            collirder = transform.GetChild(0);
            back = GetComponentInChildren<Back>();
            front = GetComponentInChildren<Front>();
        }
        StartMove();
    }

    private void SetPath()
    {
        currentPathPoint = GameManager.Instance.pathPoints[pathPointIndex];
        if (currentPathPoint != null)
        {
            currentDirection = (currentPathPoint.position - transform.position).normalized;
        }
        Rotate(currentDirection);
    }

    public void Move()
    {
        if (isMoving)
        {
            rigidbody.velocity = currentDirection * moveSpeed;
        }
    }

    private void Rotate(Vector2 currentDirection)
    {
        if (Mathf.Abs(currentDirection.x) < Mathf.Abs(currentDirection.y)) // Вертикальное движение
        {
            if (currentDirection.y < 0f || currentDirection.y > 0f)
            {
                back.DecreaseSize();
                front.DecreaseSize();
                collirder.rotation = Quaternion.Euler(0, 0, -90);
            }
        }

        else // Горизонтальное движение
        {
            if (currentDirection.x > 0f)
            {
                collirder.rotation = Quaternion.Euler(0, 0, 0);

            }
            else if (currentDirection.x < 0f)
            {
                collirder.rotation = Quaternion.Euler(0, 0, 180);
            }
            StartResetSize();
        }
    }

    public void PathPointUpdate()
    {
        if (currentPathPoint != null)
            if (Vector2.Distance(currentPathPoint.position, transform.position) <= 0.15f)
            {
                pathPointIndex++;

                if (pathPointIndex >= GameManager.Instance.pathPoints.Count)
                {
                    StopMove();
                    return;
                }
                else
                {
                    SetPath();
                }
            }
    }

    public void StartMove()
    {
        isMoving = true;
        SetPath();
    }

    public void StopMove()
    {
        isMoving = false;
        rigidbody.velocity = Vector2.zero;

        // pathPointIndex = 0;
    }

    private void StartResetSize()
    {
        back.SetBaseSize();
        front.SetBaseSize();
    }
}
