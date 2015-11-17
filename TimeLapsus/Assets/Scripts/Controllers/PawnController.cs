using UnityEngine;
using System.Collections;

public class PawnController : MonoBehaviour
{

    public delegate void ContinueWith();

    public float DestinationDelta = 0.1f;
    public float MoveSpeed = 2;

    private ContinueWith currentContinue;
    private Vector3 currentTarget;
    private Vector3 currentDirection;
    private bool isMoving;
    private Facing direction;

    private SpriteRenderer sprite;


    // Use this for initialization
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        SetNewFacing(direction);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {

            transform.position += currentDirection * MoveSpeed * Time.deltaTime;

            if (Vector3.Distance(gameObject.transform.position, currentTarget) <= DestinationDelta)
            {
                isMoving = false;
                currentContinue();

            }
        }
    }

    public void MoveTo(Vector3 target, ContinueWith nextFn)
    {
        target.z = gameObject.transform.position.z;
        if (Vector3.Distance(gameObject.transform.position, target) <= DestinationDelta)
        {
            nextFn();
            return;
        }

        currentTarget = target;
        currentContinue = nextFn;
        currentDirection = target - gameObject.transform.position;

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (currentDirection.x != 0)
            SetNewFacing(currentDirection.x < 0 ? Facing.Left : Facing.Right);

        currentDirection.Normalize();
        isMoving = true;
    }

    internal void SetNewFacing(Facing newDirection)
    {
        if (direction == newDirection)
            return;

        direction = newDirection;
        if (sprite != null)
            sprite.transform.Rotate(Vector3.up, 180, Space.Self);
    }

    internal void SetPosition(Vector3 target)
    {
        gameObject.transform.position = target;
    }
}
