using UnityEngine;
using System.Collections;

public class TestScript : ScriptWithController
{

    public string[] BooleanKeysAddedInBeginning;
    public EnumItemID[] InventoryToInit;

    // Use this for initialization
    void Start()
    {
        if (!Debug.isDebugBuild)
            return;
        

        foreach (var s in BooleanKeysAddedInBeginning)
        {
            var currentQuest = QuestController.Instance.GetCurrent();
            if (string.IsNullOrEmpty(s))
                continue;

            currentQuest.SetValue(s, true);
        }

        foreach (var i in InventoryToInit)
        {
           Controller.AddInventoryItem(i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
