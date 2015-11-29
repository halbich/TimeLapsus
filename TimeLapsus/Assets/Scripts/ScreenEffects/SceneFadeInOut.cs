using UnityEngine;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour
{
    public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.


    public FadeInOutState CurrentState;
    public bool FadeOutAtStart;


    void Start()
    {
        if (FadeOutAtStart)
        {
            GetComponent<GUITexture>().color = Color.black;
        }

    }

    void Awake()
    {
        // Set the texture so that it is the the size of the screen and covers it.
        GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
    }


    void Update()
    {
        switch (CurrentState)
        {
            case FadeInOutState.None:
                return;
            case FadeInOutState.FadeIn:
                StartScene();
                break;
            default:
                EndScene();
                break;
        }
    }


    void FadeToClear()
    {
        // Lerp the colour of the texture between itself and transparent.
        GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
    }


    void FadeToBlack()
    {
        // Lerp the colour of the texture between itself and black.
        GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.black, fadeSpeed * Time.deltaTime);
    }


    void StartScene()
    {
        // Fade the texture to clear.
        FadeToClear();

        // If the texture is almost clear...
        if (GetComponent<GUITexture>().color.a <= 0.05f)
        {
            // ... set the colour to clear and disable the GUITexture.
            GetComponent<GUITexture>().color = Color.clear;
            GetComponent<GUITexture>().enabled = false;

            CurrentState = FadeInOutState.None;
        }
    }


    public void EndScene()
    {
        // Make sure the texture is enabled.
        GetComponent<GUITexture>().enabled = true;

        CurrentState = FadeInOutState.FadeOut;

        // Start fading towards black.
        FadeToBlack();

        // If the screen is almost black...
        if (GetComponent<GUITexture>().color.a >= 0.95f)
            CurrentState = FadeInOutState.None;
    }
}