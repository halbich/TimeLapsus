using UnityEngine;
using System.Collections;

public class TestScript : ScriptWithController
{

    public string[] BooleanKeysAddedInBeginning;
    public EnumItemID[] InventoryToInit;

    // Use this for initialization
    protected override void Start()
    {
        if (!Debug.isDebugBuild)
        {
            Destroy(gameObject);
            return;
        }
        

        foreach (var s in BooleanKeysAddedInBeginning ?? new string[0])
        {
            var currentQuest = QuestController.Instance.GetCurrent();
            if (string.IsNullOrEmpty(s))
                continue;

            currentQuest.SetBoolean(s);
        }

        foreach (var i in InventoryToInit ?? new EnumItemID[0])
        {
           Controller.AddInventoryItem(i);
        }


    }


  
}
