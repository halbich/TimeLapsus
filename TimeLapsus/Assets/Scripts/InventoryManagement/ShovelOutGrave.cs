using UnityEngine;
using System.Collections;

public class ShovelOutGrave : ItemUseOnScript
{

    public string ShoveledGraveKeyName;

    public override void Use()
    {
        base.Use();
        QuestController.Instance.GetCurrent().SetBoolean(ShoveledGraveKeyName);
        var c = GetComponent<GravePresentController>();
        c.ActionOccured();
    }
}
