using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BaseController : MonoBehaviour
{

    public GameObject PlayerCharacter;

    public PawnController PlayerController;

    private static EnumLevel previousLoadedLevel;

    public EnumLevel PreviousLoadedLevel
    {
        get { return previousLoadedLevel; }
    }

    [SerializeField]
    public StartPosition[] StartPositions;

    public SceneFadeInOut Fader;

    // Use this for initialization
    private void Start()
    {
        PlayerCharacter = GameObject.FindWithTag("Player");
        if (PlayerCharacter == null)
            return;

        PlayerController = PlayerCharacter.GetComponent<PawnController>();
        var enterData = GetEnterData(PreviousLoadedLevel);
        PlayerController.SetPosition(enterData.StartPoint);
        PlayerController.SetNewFacing(enterData.Direction);

        Fader = GetComponentInChildren<SceneFadeInOut>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();

        }

    }

    public void ChangeScene(EnumLevel newLevel)
    {
        previousLoadedLevel = Statics.GetFromName(Application.loadedLevelName);
        Application.LoadLevel(newLevel.GetName());
    }



    [Serializable]
    public struct StartPosition
    {
        [SerializeField]
        public EnumLevel LevelName;

        [SerializeField]
        public Vector3 StartPoint;

        [SerializeField]
        public Facing Direction;

        public static bool operator ==(StartPosition a, StartPosition b)
        {
            // Return true if the fields match:
            return a.LevelName == b.LevelName && a.StartPoint == b.StartPoint && a.Direction == b.Direction;
        }

        public static bool operator !=(StartPosition a, StartPosition b)
        {
            return !(a == b);
        }
    }


    public Vector3 GetEnterPosition(EnumLevel level)
    {
        return GetEnterData(level).StartPoint;
    }

    public StartPosition GetEnterData(EnumLevel level)
    {
        var result = StartPositions.SingleOrDefault(e => e.LevelName == level);
        if (result != default(StartPosition)) // it is not default struct value
            return result;

        if (Debug.isDebugBuild)
        {
            Debug.LogError("nenalezen startovací objekt! " + level);
            return StartPositions.First();
        }
        else
        {
            return StartPositions.Single(e => e.LevelName == level);
        }
    }

}
