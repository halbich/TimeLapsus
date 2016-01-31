using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VideoScript : MonoBehaviour {
    public MovieTexture Movie;
    public AudioSource SoundSource;
	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.mainTexture = Movie;
        Movie.Play();
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForMovieEnd(SoundSource.clip.length));
	}

    IEnumerator WaitForMovieEnd(float movieLength)
    {
        yield return new WaitForSeconds(movieLength);
        SceneManager.LoadScene(EnumLevel.RiverSide.GetName());
    }


    private void Update()
    {
        if (!Input.anyKey)
            return;

        GetComponent<AudioSource>().Stop();
        Movie.Stop();

        //    StopAllCoroutines();
        SceneManager.LoadScene(EnumLevel.RiverSide.GetName());
    }
}
