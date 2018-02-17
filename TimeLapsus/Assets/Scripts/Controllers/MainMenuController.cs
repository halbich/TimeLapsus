using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Texture2D cursor;

    void Start()
    {
        Cursor.visible = true;
        var hotSpot = new Vector2(cursor.width / 2f, cursor.height / 2f);
        Cursor.SetCursor(cursor, hotSpot, CursorMode.Auto);



        if (AudioListener.volume == 0f)
            FindObjectOfType<Toggle>().isOn = true;
    }

    public AmbientMusicController musicController;

    public void StartGame()
    {
		StartCoroutine(setLevel(EnumLevel.Intro, 1));
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
        if (toggleOn)
            GetComponent<AudioSource>().Play();
        AudioListener.volume = toggleOn ? 0 : 1;
        if (!toggleOn)
            GetComponent<AudioSource>().Play();
    }
}