using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VideoScript : MonoBehaviour {
    public AudioSource SoundSource;
    //HACK: So, for some wierd reason a day before presentation the intro stops working. Great. Length is no longer correctly returned.
    public float MovieDuration;
	// Use this for initialization
	void Start () {
        GetComponent<AudioSource>().Play();
        //StartCoroutine(WaitForMovieEnd(MovieDuration));
        StartCoroutine(WaitForMovieEnd(SoundSource.clip.length));
    }

    IEnumerator WaitForMovieEnd(float movieLength)
    {
        yield return new WaitForSeconds(movieLength);
        GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene(EnumLevel.RiverSide.GetName());
    }


    private void Update()
    {
        if (!Input.GetKeyUp(KeyCode.Escape))
            return;

        GetComponent<AudioSource>().Stop();

        //    StopAllCoroutines();
        SceneManager.LoadScene(EnumLevel.RiverSide.GetName());
    }
}
