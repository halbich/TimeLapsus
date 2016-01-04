using UnityEngine;
using System.Collections;

public class BridgeBehavior : MonoBehaviour
{

    public GameObject originalWalkable;
    public GameObject originalTrigger;
    public string IsBridgeBuiltVarName;
    private Quest currentQuest = QuestController.Instance.GetCurrent();

    // Use this for initialization
    void Start()
    {

        if (currentQuest.GetBoolean(IsBridgeBuiltVarName))
        {
            originalWalkable.SetActive(false);
            originalTrigger.SetActive(false);

        }
        else
        {
            gameObject.SetActive(false);
        }

    }


}
