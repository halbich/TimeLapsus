using UnityEngine;
using System.Collections;

public class MoveLeftRight : MonoBehaviour
{

    public PawnController Pawn;
    private Vector3[] points;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(str());


    }

    IEnumerator str()
    {
        yield return new WaitForSeconds(1);
        var pawnLoc = Pawn.transform.position;
        points = new Vector3[2];
        points[0] = points[1] = pawnLoc;
        points[1].x *= -1;

        var index = 0;

        while (true)
        {
            index %= 2;
            Pawn.MoveTo(points[index], null);
            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => !Pawn.isMoving);
            index++;
        }
    }



}
