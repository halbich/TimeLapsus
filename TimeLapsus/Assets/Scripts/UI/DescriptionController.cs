using UnityEngine;
using UnityEngine.UI;

public class DescriptionController : MonoBehaviour
{
    bool totalFreeze = false;
    // Use this for initialization
    private void Start()
    {
        SetDescription("", true);
    }

    private bool descriptionFrozen;

    public void FreezeForItemUse()
    {
        descriptionFrozen = true;
    }

    public void UnfreezeForItemUse()
    {
        descriptionFrozen = false;
    }
    public void Freeze()
    {
        descriptionFrozen = true;
        totalFreeze = true;
    }
    public void Unfreeze()
    {
        descriptionFrozen = false;
        totalFreeze = false;
    }
    public void SetDescription(string description, bool isItemUse)
    {
        if (descriptionFrozen && (!isItemUse || totalFreeze))
            return;
        var text = string.IsNullOrEmpty(description) ? "" : TextController.Instance.GetText(description);
        GetComponent<Text>().text = text;
    }
}