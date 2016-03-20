using UnityEngine;

public class SceneFadeInOut : ScriptWithController
{
    public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.

    public FadeInOutState CurrentState;
    public bool FadeOutAtStart;


    protected override void Start()
    {
        base.Awake();
        if (FadeOutAtStart)
        {
            GetComponent<GUITexture>().color = Color.black;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        // Set the texture so that it is the the size of the screen and covers it.
        GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);

        if (Statics.TimelineChanged)
        {
            FadeOutAtStart = false;
        }

    }

    private void Update()
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

    private void FadeToClear()
    {
        // Lerp the colour of the texture between itself and transparent.
        GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    private void FadeToBlack()
    {
        // Lerp the colour of the texture between itself and black.
        GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.black, fadeSpeed * Time.deltaTime);
    }

    private void StartScene()
    {
        if (Controller != null)Controller.DisableInput();
        // Fade the texture to clear.
        FadeToClear();

        // If the texture is almost clear...
        if (!(GetComponent<GUITexture>().color.a <= 0.05f))
            return;

        // ... set the colour to clear and disable the GUITexture.
        GetComponent<GUITexture>().color = Color.clear;
        GetComponent<GUITexture>().enabled = false;

        CurrentState = FadeInOutState.None;
        if (Controller != null)
        {
            Controller.EnableInput();
            if (!Statics.TimelineChanged) Controller.SignalLoadComplete();
        }
    }

    public void EndScene()
    {
        Controller.DisableInput();
        // Make sure the texture is enabled.
        GetComponent<GUITexture>().enabled = true;

        CurrentState = FadeInOutState.FadeOut;

        // Start fading towards black.
        FadeToBlack();

        // If the screen is almost black...
        if (GetComponent<GUITexture>().color.a >= 0.95f)
        {
            CurrentState = FadeInOutState.None;
            Controller.EnableInput();
        }
    }
}