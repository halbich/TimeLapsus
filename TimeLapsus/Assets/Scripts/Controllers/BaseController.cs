using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BaseController : MonoBehaviour
{

    internal bool DialogueActive = false;

    public GameObject PlayerCharacter;

    public PawnController PlayerController;

    public CursorManager CursorManager;

    public DialogController DialogController;

    public float CharacterZPosition = -0.1f;
    
    private static EnumLevel previousLoadedLevel;

    public string DisplayedDescription = "";

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
        CursorManager = GetComponent<CursorManager>();
        DialogController = GetComponent<DialogController>();


    }

    private void Awake()
    {
        PlayerCharacter = GameObject.FindWithTag("Player");
        if (PlayerCharacter == null)
            return;

        PlayerController = PlayerCharacter.GetComponent<PawnController>();

        var startObjects = GameObject.FindGameObjectsWithTag("Respawn").ToList();

        startPositions = startObjects.Select(e => e.GetComponent<RespawnPointScript>().GetPoint(e, CharacterZPosition)).ToList();
        
        foreach (var startObject in startObjects)
        {
            Destroy(startObject);
        }

        var enterData = GetEnterData(PreviousLoadedLevel);
        PlayerController.SetInitPosition(enterData.StartPoint);
        PlayerController.SetNewFacing(enterData.Direction);


        var bckgrnd = GameObject.FindGameObjectWithTag("Background");
        if(bckgrnd != null)
        {
            bckgrnd.GetComponent<SpriteRenderer>().color = Color.white;
        }
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
        var startPoint = GetEnterData(level).StartPoint;
        return startPoint;
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


