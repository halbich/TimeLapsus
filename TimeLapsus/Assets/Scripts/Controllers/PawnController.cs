using UnityEngine;
using System.Collections;

public class PawnController : MonoBehaviour
{

    public delegate void ContinueWith();

    public float DestinationDelta = 0.1f;
    public float MoveSpeed = 2;
    public float DestinationOffsetY = 1.2f;

    private ContinueWith currentContinue;
    private Vector3 currentTarget;
    private Vector3 currentDirection;
    private bool isMoving;
    private Facing direction;
    private Facing? afterLoadFacing;

    private SpriteRenderer sprite;
    private Animator animator;


    // Use this for initialization
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        if (afterLoadFacing.HasValue)
        {
            SetNewFacing(afterLoadFacing.Value);
            afterLoadFacing = default(Facing?);
        }

        animator = GetComponent<Animator>();
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
                animator.SetTrigger("WalkEnd");
                currentContinue();

            }
        }
    }

    public void MoveTo(Vector3 target, ContinueWith nextFn)
    {
        target.z = gameObject.transform.position.z;
        target.y += DestinationOffsetY;
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
        animator.SetTrigger("WalkStart");
    }

    internal void SetNewFacing(Facing newDirection)
    {
        if (sprite == null)
        {
            afterLoadFacing = newDirection;
            return;
        }


        if (direction == newDirection)
            return;
        Debug.Log(string.Format("Facing, {0} -> {1} ", direction, newDirection));
        Debug.Log(sprite.transform.rotation);

        direction = newDirection;
        sprite.transform.Rotate(Vector3.up, 180, Space.Self);
    }

    internal void SetInitPosition(Vector3 target)
    {
        target.y += DestinationOffsetY;
        gameObject.transform.position = target ;
    }
}
