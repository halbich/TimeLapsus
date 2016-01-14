using UnityEngine;
using System.Collections;

public class ScreenChanger : ScriptWithController
{


    private GUITexture levelTexture;
    private GUITexture otherLevelTexture;

    private readonly Color invisible = new Color(0.5f, 0.5f, 0.5f, 0f);
    private readonly Color visible = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    private readonly Color halfVisible = new Color(0.5f, 0.5f, 0.5f, 0.25f);

    [HideInInspector]
    public bool CanContinueWithLoad;

    public float ChangeTime = 3;
    public float StartTime = 1;



    protected override void Awake()
    {
        base.Awake();
        // Set the texture so that it is the the size of the screen and covers it.
        //GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
        var subScreens = GetComponentsInChildren<GUITexture>();

        foreach (var subScreen in subScreens)
        {
            if (subScreen.name == "Level")
                levelTexture = subScreen;

            if (subScreen.name == "Other")
                otherLevelTexture = subScreen;
        }

        levelTexture.pixelInset = new Rect(Screen.width / 2f, Screen.height / 2f, 0,0);
        levelTexture.color = invisible;

        otherLevelTexture.pixelInset = new Rect(levelTexture.pixelInset);
        otherLevelTexture.color = invisible;

        Debug.LogFormat("ScrCh: {0}", Statics.TimelineChanged);

        SetActive(Statics.TimelineChanged);

    }

    // Use this for initialization
    protected override void  Start()
    {
        base.Start();
        if (Statics.TimelineChanged)
            StartCoroutine(continueWithChange());
    }

    IEnumerator continueWithChange()
    {
        levelTexture.color = halfVisible;
        otherLevelTexture.color = visible;

        var totalTime = ChangeTime / 2f;

        float elapsed = 0;
        while (elapsed < totalTime)
        {
        if (Controller) Controller.DisableInput();

            levelTexture.color = Color.Lerp(halfVisible, visible, elapsed / totalTime);
            elapsed += Time.deltaTime;
            yield return null;

        }

        otherLevelTexture.color = invisible;
        float startElapsed = 0;
        while (startElapsed < StartTime)
        {
            if (Controller) 
                Controller.DisableInput();
            levelTexture.color = Color.Lerp(visible, invisible, startElapsed / StartTime);
            startElapsed += Time.deltaTime;
            yield return null;

        }

        if (Controller)
            Controller.EnableInput();
        SetActive(false);
    }


    internal void Activate()
    {
        SetActive(Statics.TimelineChanged);
        StartCoroutine(ChangeScreens());
    }

    IEnumerator ChangeScreens()
    {
        otherLevelTexture.color = invisible;
        float startElapsed = 0;
        while (startElapsed < StartTime)
        {

            if (Controller) Controller.DisableInput();
            levelTexture.color = Color.Lerp(invisible, visible, startElapsed / StartTime);
            startElapsed += Time.deltaTime;
            yield return null;

        }

        otherLevelTexture.color = visible;

        var totalTime = ChangeTime / 2f;
        float elapsed = 0;
        while (elapsed < totalTime)
        {
            if (Controller) Controller.DisableInput();

            levelTexture.color = Color.Lerp(visible, halfVisible, elapsed / totalTime);
            elapsed += Time.deltaTime;
            yield return null;

        }

        if (Controller) Controller.EnableInput();

        CanContinueWithLoad = true;
    }

    internal void SetActive(bool active)
    {
        levelTexture.gameObject.SetActive(active);
        otherLevelTexture.gameObject.SetActive(active);
    }
}
