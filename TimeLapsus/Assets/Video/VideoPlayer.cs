using UnityEngine;
using System.Collections;

public class VideoPlayer : MonoBehaviour {

    public MovieTexture movTexture;

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.mainTexture = movTexture;
        movTexture.Play();
	}
	

}
