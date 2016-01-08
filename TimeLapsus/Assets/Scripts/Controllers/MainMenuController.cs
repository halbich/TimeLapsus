using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    void Start()
    {
        if (AudioListener.volume == 0f)
            FindObjectOfType<Toggle>().isOn = true;
    }

    public AmbientMusicController musicController;

    public void StartGame()
    {
        StartCoroutine(setLevel(EnumLevel.RiverSide, 1));
    }

    IEnumerator setLevel(EnumLevel level, float time)
    {
        GetComponent<AudioSource>().Play();
        musicController.QuietDown(time);
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(level.GetName());
    }

    public void AboutGame()
    {
        StartCoroutine(setLevel(EnumLevel.About, 1));

    }

    public void QuitGame()
    {
        musicController.QuietDown(1);
        GetComponent<AudioSource>().Play();
        Application.Quit();
    }

    public void MuteAudio(bool toggleOn)
    {

        AudioListener.volume = toggleOn ? 0 : 1;

    }
}