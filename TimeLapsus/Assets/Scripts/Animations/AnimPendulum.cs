using UnityEngine;
using System.Collections;

public class AnimPendulum : MonoBehaviour
{
    public float Angle = 5f;
    private float fTimer;
    private Vector3 v3T = Vector3.zero;


    void Update()
    {
        float f = (Mathf.Sin(fTimer * Mathf.PI - Mathf.PI / 2.0f) + 1.0f) / 2.0f;

        v3T.Set(0.0f, 0.0f, Mathf.Lerp(Angle, -Angle, f));
        transform.eulerAngles = v3T;
        fTimer += Time.deltaTime;
    }



}