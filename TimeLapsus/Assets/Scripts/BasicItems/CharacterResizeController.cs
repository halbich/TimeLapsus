using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class CharacterResizeController : MonoBehaviour
{

    private List<CharacterScale> changeSizePoints;
    private CharacterScale firstPoint;
    private CharacterScale lastPoint;

    // Use this for initialization
    void Awake()
    {
        var allPoints = GameObject.FindGameObjectsWithTag("SizeDefine").OrderBy(e => e.transform.position.y).ToList();

        if (!allPoints.Any())
        {
            Debug.LogError("Not size points defined!");
            return;
        }

        changeSizePoints = new List<CharacterScale>(allPoints.Count);

        var lastPt = allPoints[0].transform;
        changeSizePoints.Add(new SimpleCharacterScale(lastPt));

        if (allPoints.Count == 1)
        {
            updateBorders(allPoints);
            return;
        }




        for (int i = 1; i < allPoints.Count(); i++)
        {
            var point = allPoints[i].transform;
            changeSizePoints.Add(new BinaryCharacterScale(lastPt, point));
            lastPt = point;

        }

        var lastBord = allPoints[allPoints.Count - 1].transform;
        changeSizePoints.Add(new SimpleCharacterScale(lastBord));
        updateBorders(allPoints);

    }

    private void updateBorders(List<GameObject> pAllPoints)
    {
        firstPoint = changeSizePoints[0];
        lastPoint = changeSizePoints[changeSizePoints.Count - 1];

        if(!Application.isPlaying)
            return;
        

        for (int i = 0; i < pAllPoints.Count; i++)
        {
            Destroy(pAllPoints[i]);
        }
    }

    internal Vector3 GetScale(Vector3 posisiton)
    {
        if (changeSizePoints.Count == 0)
            throw new Exception("No change size points.");

        if (posisiton.y <= firstPoint.YValueTopBorder)
        {
            return firstPoint.GetScale(posisiton.y) * Vector3.one;
        }

        if (posisiton.y >= lastPoint.YValueTopBorder)
        {
            return lastPoint.GetScale(posisiton.y) * Vector3.one;
        }



        var last = changeSizePoints[1];
        for (int i = 1; i < changeSizePoints.Count(); i++)
        {
            last = changeSizePoints[i];
            if (posisiton.y <= last.YValueTopBorder)
                break;

        }

        return last.GetScale(posisiton.y) * Vector3.one;
    }


    private abstract class CharacterScale
    {
        public float YValueTopBorder;

        public abstract float GetScale(float y);

        protected CharacterScale(float yValue)
        {
            YValueTopBorder = yValue;
        }


    }

    private sealed class SimpleCharacterScale : CharacterScale
    {
        private Vector3 scalePoint1;
        private Vector3 scalePoint1Size;

        public SimpleCharacterScale(Transform t1)
            : base(t1.position.y)
        {
            scalePoint1 = t1.position;
            scalePoint1Size = t1.localScale;
        }

        public override float GetScale(float y)
        {
            return scalePoint1Size.y;
        }
    }

    private sealed class BinaryCharacterScale : CharacterScale
    {
        private Vector3 scalePoint1;
        private Vector3 scalePoint1Size;
        private Vector3 scalePoint2;
        private Vector3 scalePoint2Size;

        public BinaryCharacterScale(Transform t1, Transform t2)
            : base(t2.position.y)
        {
            scalePoint1 = t1.position;
            scalePoint1Size = t1.localScale;
            scalePoint2 = t2.position;
            scalePoint2Size = t2.localScale;
        }

        public override float GetScale(float y)
        {
            var k = (scalePoint2Size.y - scalePoint1Size.y) / (scalePoint2.y - scalePoint1.y);

            return k * (y - scalePoint1.y) + scalePoint1Size.y;
        }
    }
}
