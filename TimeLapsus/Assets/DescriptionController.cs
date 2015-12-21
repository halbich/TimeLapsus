using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DescriptionController : MonoBehaviour {
	// Use this for initialization
	void Start () {
        SetDescription("", true);
	}
    bool descriptionFrozen = false;
    public void FreezeForItemUse()
    {
        descriptionFrozen = true;
    }

    public void UnfreezeForItemUse()
    {
        descriptionFrozen = false;
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void SetDescription(string name, bool isItemUse)
    {
        if (descriptionFrozen && !isItemUse) return; 
        GetComponent<Text>().text = string.IsNullOrEmpty(name) ? "" : TextController.Instance.GetText(name);
    }
}
