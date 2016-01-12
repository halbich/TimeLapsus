using UnityEngine;
using System.Collections;

public class animHat : MonoBehaviour
{

    [Tooltip("Minimální doba čekání - dole")]
    public float MinWait;

    [Tooltip("Maximální doba čekání - dole")]
    public float MaxWait;

    [Tooltip("Velikost posunu vzhůru")]
    public float YTranslationMax;

    [Tooltip("Rychlost posunu vzhůru")]
    public float YUpSpeed;

    [Tooltip("Minimální doba čekání - nahoře")]
    public float UpMinWait;

    [Tooltip("Maximální doba čekání - nahoře")]
    public float UpMaxWait;

    [Tooltip("Rychlost posunu dolů")]
    public float YDownSpeed;


    private Vector3 initPosition;
    private Vector3 targetPosition;

    private const float EPSILON = 0.1f;


    // Use this for initialization
    void Start()
    {
        initPosition = transform.localPosition;
        targetPosition = initPosition + Vector3.up * YTranslationMax;
        StartCoroutine(animation());
    }

    IEnumerator animation()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(MinWait, MaxWait));

            while (transform.localPosition.y < targetPosition.y)
            {
                transform.Translate(Vector3.up * YUpSpeed * Time.deltaTime, Space.Self);
                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(UpMinWait, UpMaxWait));

            while (transform.localPosition.y > initPosition.y)
            {
                transform.Translate(Vector3.down * YDownSpeed * Time.deltaTime, Space.Self);
                yield return null;
            }
        }
    }

}
