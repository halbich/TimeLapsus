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

    public SceneFadeInOut Fader;

    [SerializeField]
    private List<RespawnPoint> startPositions;

    // Use this for initialization
    private void Start()
    {

        Fader = GetComponentInChildren<SceneFadeInOut>();


    }

    private void Awake()
    {
        PlayerCharacter = GameObject.FindWithTag("Player");
        if (PlayerCharacter == null)
            return;

        PlayerController = PlayerCharacter.GetComponent<PawnController>();

        var startObjects = GameObject.FindGameObjectsWithTag("Respawn").ToList();

        startPositions = startObjects.Select(e => e.GetComponent<RespawnPointScript>().GetPoint(e)).ToList();
        foreach (var startObject in startObjects)
        {
            Destroy(startObject);
        }

        var enterData = GetEnterData(PreviousLoadedLevel);
        PlayerController.SetInitPosition(enterData.StartPoint);
        PlayerController.SetNewFacing(enterData.Direction);

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






    public Vector3 GetEnterPosition(EnumLevel level)
    {
        return GetEnterData(level).StartPoint;
    }

    public RespawnPoint GetEnterData(EnumLevel level)
    {
        var result = startPositions.SingleOrDefault(e => e.LevelName == level);
        if (result != default(RespawnPoint)) // it is not default struct value
            return result;

        if (Debug.isDebugBuild)
        {
            Debug.LogWarning("nenalezen startovací objekt! " + level);
            return startPositions.First();
        }
        return startPositions.Single(e => e.LevelName == level);
    }
}


