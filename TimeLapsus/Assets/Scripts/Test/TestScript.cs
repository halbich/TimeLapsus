using System.Collections.Generic;
using UnityEngine;

public class TestScript : ScriptWithController
{
    private List<string> alwaysList = new List<string>
    {
        "hasTriggeredAfterTeleportDialog"
    };

    public string[] BooleanKeysAddedInBeginning;
    public EnumItemID[] InventoryToInit;

    // Use this for initialization
    protected override void Awake()
    {
        if ( !Debug.isDebugBuild)
        {
            Destroy(gameObject);
            return;
        }

        base.Awake();

        foreach (var s in alwaysList ?? new List<string>())
        {
            var currentQuest = QuestController.Instance.GetCurrent();
            if (!currentQuest.GetBoolean(s))
                currentQuest.SetBoolean(s);
        }

        foreach (var s in BooleanKeysAddedInBeginning ?? new string[0])
        {
            var currentQuest = QuestController.Instance.GetCurrent();
            if (string.IsNullOrEmpty(s))
                continue;
            if (!currentQuest.GetBoolean(s))
                currentQuest.SetBoolean(s);
        }
    }

    protected override void Start()
    {
        base.Start();
        foreach (var i in InventoryToInit ?? new EnumItemID[0])
        {
            if (!Controller.HasInventoryItem(i))
                Controller.AddInventoryItem(i);
        }
    }
}