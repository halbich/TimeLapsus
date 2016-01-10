using UnityEngine;
using System.Collections;

public class vrtule : MonoBehaviour
{

    public float Rate;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        transform.Rotate(Vector3.forward, Rate * Time.deltaTime);
	}
}
