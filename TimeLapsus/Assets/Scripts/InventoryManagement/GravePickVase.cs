using UnityEngine;
using System.Collections;

public class GravePickVase : PickableItem {

    public ShovelOutGrave ShovelScript;

    internal override void IsInspected()
    {
        if (currentQuest.GetBoolean(ShovelScript.ShoveledGraveKeyName))
            base.IsInspected();
    }
}
