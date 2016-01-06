using UnityEngine;

public class GraveWallBehavior : MonoBehaviour
{
    public Sprite QuestDoneSprite;
    public string IsBridgeBuiltVarName;
    private Quest currentQuest = QuestController.Instance.GetCurrent();

    // Use this for initialization
    private void Start()
    {
        if (currentQuest.GetBoolean(IsBridgeBuiltVarName))
        {
            GetComponent<SpriteRenderer>().sprite = QuestDoneSprite;
        }
    }
}