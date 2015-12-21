﻿using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseController : MonoBehaviour
{

    public GameObject InventoryItemTemplate;

    public GameObject PlayerCharacter;

    public PawnController PlayerController;

    public CursorManager CursorManager;

    public float CharacterZPosition = -0.1f;

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
        CursorManager = GetComponent<CursorManager>();


        PlayerController = PlayerCharacter.GetComponent<PawnController>();
        if (PlayerController == null)
        {
            Debug.LogError("No controller");
            return;
        }

        var enterData = GetEnterData(PreviousLoadedLevel);
        PlayerController.SetInitPosition(enterData.StartPoint);
        PlayerController.SetNewFacing(enterData.Direction);
    }

    private void Awake()
    {
        PlayerCharacter = GameObject.FindWithTag("Player");
        if (PlayerCharacter == null)
            return;


        var startObjects = GameObject.FindGameObjectsWithTag("Respawn").ToList();

        startPositions = startObjects.Select(e => e.GetComponent<RespawnPointScript>().GetPoint( CharacterZPosition)).ToList();

        foreach (var startObject in startObjects)
        {
            Destroy(startObject);
        }



        var bckgrnd = GameObject.FindGameObjectWithTag("Background");
        if (bckgrnd != null)
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

        if (!Debug.isDebugBuild)
            return startPositions.Single(e => e.LevelName == level);

        Debug.LogWarning("nenalezen startovací objekt! " + level);
        return startPositions.First();
    }

    public void AddInventoryItem(EnumItemID toAdd)
    {
        Statics.Inventory.Add(Statics.AllInventoryItems[toAdd]);
        FindObjectOfType<InventoryController>().UpdateInventory();
    }
}


