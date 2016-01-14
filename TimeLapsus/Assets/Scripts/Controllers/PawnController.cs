using System.Collections;
using UnityEngine;

public class PawnController : MonoBehaviour
{
    public delegate void ContinueWith();

    public float DestinationDelta = 0.1f;

    [HideInInspector]
    public float MoveSpeed = 2;
    public float DestinationOffsetY = 1.2f;

    public float MoveSpeedMultiplicator = 1;

    public Pathfinder pathfinder;
    private ContinueWith currentContinue;
    private Vector3 currentTarget;
    private Vector3 currentDirection;
    public bool isMoving;
    private Facing direction;
    private Facing? afterLoadFacing;

    private SpriteRenderer sprite;
    private Animator animator;
    private float currentRemainingDistance;

    private Vector2[] currentPathToWalk;
    int currentPathToWalkIndex;

    private StepSoundRegion currentStepSoundRegion;
    public float StepsWaitTime = 0.4f;

    // Use this for initialization
    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        pathfinder = GetComponent<Pathfinder>();
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

        animator.SetFloat("MoveSpeedMultiplicator", MoveSpeedMultiplicator);
        transform.position += currentDirection * MoveSpeed * MoveSpeedMultiplicator * Time.deltaTime;

        var newRemainingDistance = Vector3.Distance(gameObject.transform.position, currentTarget);

        if (newRemainingDistance > DestinationDelta &&
            (newRemainingDistance - currentRemainingDistance) < 0) // we are getting closer to destination
        {
            currentRemainingDistance = newRemainingDistance;
            return;
        }

        currentContinue();
    }



    public void MoveTo(Vector3 target, ContinueWith nextFn)
    {
        currentPathToWalk = pathfinder.GetPath(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(target.x, target.y));
        currentPathToWalkIndex = 0;
        MoveToInternal(currentPathToWalk[0], () =>
        {
            InternalMoveContinueWith(nextFn);
        });
    }

    private void InternalMoveContinueWith(ContinueWith realContinueWith)
    {
        ++currentPathToWalkIndex;
        if (currentPathToWalk.Length <= currentPathToWalkIndex)
        {
            if (isMoving)
            {
                isMoving = false;
                animator.SetTrigger("WalkEnd");
            }
            if (realContinueWith != null)
                realContinueWith();
            return;
        }
        MoveToInternal(currentPathToWalk[currentPathToWalkIndex], () =>
        {
            InternalMoveContinueWith(realContinueWith);
        });
    }

    private void MoveToInternal(Vector2 target2D, ContinueWith nextFn)
    {
        Vector3 target = new Vector3(target2D.x, target2D.y, gameObject.transform.position.z);
        target.y += DestinationOffsetY;

        currentRemainingDistance = Vector3.Distance(gameObject.transform.position, target);

        if (currentRemainingDistance <= DestinationDelta)
        {
            nextFn();
            return;
        }

        currentTarget = target;
        currentContinue = nextFn;
        currentDirection = new Vector3(target.x, target.y, gameObject.transform.position.z) - gameObject.transform.position;

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (currentDirection.x != 0)
            SetNewFacing(currentDirection.x < 0 ? Facing.Left : Facing.Right);

        currentDirection.Normalize();
        if (!isMoving)
        {
            animator.SetTrigger("WalkStart");
            isMoving = true;
            StartCoroutine(playSteps());
        }

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

    internal void RegisterStepSoundRegion(StepSoundRegion stepSoundRegion)
    {
        currentStepSoundRegion = stepSoundRegion;
    }

    IEnumerator playSteps()
    {
        if (currentStepSoundRegion == null)
        {
            Debug.LogError("SoundRegion is null, waiting for it");
            yield return new WaitWhile(() => currentStepSoundRegion == null);
        }


        while (isMoving && currentStepSoundRegion != null)
        {
            currentStepSoundRegion.PlaySound();
            yield return new WaitForSeconds(StepsWaitTime);
        }
    }
}