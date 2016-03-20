using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HintController : ScriptWithController
{
    public bool IsHintEnabled = false;
    public bool IsHintActive;
    public GameObject HintTemplate;

    void Update()
    {
        IsHintActive = (Input.GetKey(KeyCode.Space)) && IsHintEnabled;
    }    
}