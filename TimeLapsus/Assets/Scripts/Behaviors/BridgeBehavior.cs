using UnityEngine;

public class BridgeBehavior : MonoBehaviour
{
    public GameObject originalWalkable;
    public GameObject originalTrigger;
    public string IsBridgeBuiltVarName;
    private Quest currentQuest = QuestController.Instance.GetCurrent();

    // Use this for initialization
    private void Start()
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