using UnityEngine;

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
    private float currentRemainingDistance;

    // Use this for initialization
    private void Start()
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
    private void Update()
    {
        if (!isMoving)
            return;

        transform.position += currentDirection * MoveSpeed * Time.deltaTime;

        var newRemainingDistance = Vector3.Distance(gameObject.transform.position, currentTarget);

        if (newRemainingDistance > DestinationDelta &&
            (newRemainingDistance - currentRemainingDistance) < 0) // we are getting closer to destination
        {
            currentRemainingDistance = newRemainingDistance;
            return;
        }

        isMoving = false;
        animator.SetTrigger("WalkEnd");
        //Debug.Log("WalkEnd");
        currentContinue();
    }

    public void MoveTo(Vector3 target, ContinueWith nextFn)
    {
        target.z = gameObject.transform.position.z;
        target.y += DestinationOffsetY;

        currentRemainingDistance = Vector3.Distance(gameObject.transform.position, target);

        if (currentRemainingDistance <= DestinationDelta)
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
        if (!isMoving)
        {
            animator.SetTrigger("WalkStart");
            //Debug.Log("WalkStart");
        }
        isMoving = true;

        //if (Debug.isDebugBuild)
        //{
        //    gameObject.transform.position = target;
        //}
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

        direction = newDirection;
        sprite.transform.Rotate(Vector3.up, 180, Space.Self);
    }

    internal void SetInitPosition(Vector3 target)
    {
        target.y += DestinationOffsetY;
        gameObject.transform.position = target;
    }
}