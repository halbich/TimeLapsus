using System.Collections;
using UnityEngine;

public class ShovelOutGrave : ItemUseOnScript
{
    public string ShoveledGraveKeyName;

    public override void Use()
    {
        if (ObjectPoint == null)
        {
            Debug.LogError("No object point defined!");
            return;
        }


        Controller.PlayerController.MoveTo(ObjectPoint.StartPoint, () =>
        {
            Controller.PlayerController.SetNewFacing(ObjectPoint.Direction);
            base.Use();
            QuestController.Instance.GetCurrent().SetBoolean(ShoveledGraveKeyName);
            StartCoroutine(useEnumerator());
        });

       
    }

    IEnumerator useEnumerator()
    {
        base.Use();
        var c = GetComponent<GravePresentController>();
        var asource = GetComponent<AudioSource>();
        Controller.PlayerCharacter.GetComponent<Animator>().SetTrigger("Dig");
        asource.Play();
        yield return new WaitForSeconds(2.5f);
        c.ActionOccured();
    }
}