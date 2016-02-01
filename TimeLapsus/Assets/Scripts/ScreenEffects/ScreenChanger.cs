using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenChanger : ScriptWithController
{


    private readonly Color invisible = new Color(0.5f, 0.5f, 0.5f, 0f);
    private readonly Color visible = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    private readonly Color halfVisible = new Color(0.5f, 0.5f, 0.5f, 0.25f);

    [HideInInspector]
    public bool CanContinueWithLoad;

    public float ChangeTime = 8;
    public float StartTime = 1;

    public List<GUITexture> SubObjects;



    protected override void Awake()
    {
        base.Awake();
        // Set the texture so that it is the the size of the screen and covers it.
        //GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);

        foreach (var texture in SubObjects)
        {
            texture.pixelInset = new Rect(Screen.width / 2f, Screen.height / 2f, 0, 0);
            texture.color = invisible;
        }

        SetActive(Statics.TimelineChanged);

    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        if (Statics.TimelineChanged)
        {
            if (Controller)
                Controller.DisableInput(5);
            StartCoroutine(continueWithChange());
        }

    }

    IEnumerator continueWithChange()
    {
        if (Controller)
            Controller.DisableInput(5);

        foreach (var texture in SubObjects)
        {
            texture.color = visible;
        }

        
        var max = SubObjects.Count - 1;
        SubObjects[max].color = halfVisible;
        for (var i = max; i >= 0; i--)
        {
            var texture = SubObjects[i];
            float startElapsed = 0;
            var startTime = i == max ? StartTime / 2f : StartTime;
            var startColor = i == max ? halfVisible : visible;

            Debug.LogFormat("{0}, {1}", startTime, startColor);
            while (startElapsed < startTime)
            {
                if (Controller)
                    Controller.DisableInput(5);
                texture.color = Color.Lerp(startColor, invisible, startElapsed / startTime);
                startElapsed += Time.deltaTime;
                yield return null;
            }
        }

        if (Controller)
            Controller.EnableInput(5);
        SetActive(false);
        Controller.SignalLoadComplete();
    }


    internal void Activate()
    {
        SetActive(Statics.TimelineChanged);
        StartCoroutine(ChangeScreens());
    }

    IEnumerator ChangeScreens()
    {
        if (Controller)
            Controller.DisableInput(1000);

        for (var i = 0; i < SubObjects.Count; i++)
        {
            var texture = SubObjects[i];
            float startElapsed = 0;
            var startTime = i == SubObjects.Count-1 ? StartTime/2f : StartTime;
            var finalColor = i == SubObjects.Count-1 ? halfVisible : visible;

            Debug.LogFormat("{0}, {1}", startTime, finalColor);

            while (startElapsed < startTime)
            {
                if (Controller)
                    Controller.DisableInput(1000);
                texture.color = Color.Lerp(invisible, finalColor, startElapsed / startTime);
                startElapsed += Time.deltaTime;
                yield return null;
            }
        }

        //otherLevelTexture.color = invisible;

        //otherLevelTexture.color = visible;

        //var totalTime = ChangeTime / 2f;
        //float elapsed = 0;
        //while (elapsed < totalTime)
        //{
        //    if (Controller)
        //        Controller.DisableInput();

        //    levelTexture.color = Color.Lerp(visible, halfVisible, elapsed / totalTime);
        //    elapsed += Time.deltaTime;
        //    yield return null;

        //}

        //if (Controller)
        //    Controller.EnableInput();

        CanContinueWithLoad = true;
    }

    internal void SetActive(bool active)
    {
        foreach (var subObject in SubObjects)
        {
            subObject.gameObject.SetActive(active);
        }
    }
}
