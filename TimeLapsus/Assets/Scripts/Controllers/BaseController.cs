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

    // Use this for initialization
    void Start()
    {
        PlayerCharacter = GameObject.FindWithTag("Player");
        if (PlayerCharacter == null)
            return;
        PlayerController = PlayerCharacter.GetComponent<PawnController>();
        PlayerController.SetPosition(GetEnterPosition(PreviousLoadedLevel));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();

        }

    }

    public void ChangeScene(EnumLevel newLevel)
    {
        previousLoadedLevel = Statics.GetFromName(Application.loadedLevelName);
        Debug.Log(PreviousLoadedLevel);
        Application.LoadLevel(newLevel.GetName());
    }



    [Serializable]
    public struct StartPosition
    {
        [SerializeField]
        public EnumLevel LevelName;

        [SerializeField]
        public Vector3 StartPoint;

    }

    public Vector3 GetEnterPosition(EnumLevel level)
    {
        return StartPositions.Single(e => e.LevelName == level).StartPoint;

    }
}
