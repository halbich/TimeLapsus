using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialoguePanel : ScriptWithController {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Controller.DialogController.DialogueText != "")
        {
            GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.7f);
        }
        else
        {
            GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.0f);
        }
	}
}
