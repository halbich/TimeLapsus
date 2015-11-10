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
    private bool isMoving = false;
    

    // Use this for initialization
    void Start()
    {
       
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
        currentDirection.Normalize();
        isMoving = true;
    }

    internal void SetPosition(Vector3 target)
    {
        gameObject.transform.position = target;
    }
}
