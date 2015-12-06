using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DescriptionText : ScriptWithController {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = Controller.DisplayedDescription;
	}
}
