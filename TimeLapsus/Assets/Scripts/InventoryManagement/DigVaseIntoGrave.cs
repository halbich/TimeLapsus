using System.Collections;
using UnityEngine;

public class DigVaseIntoGrave : ItemUseOnScript
{
    public string BuriedVaseVarName;
    public string VaseBuryDialog;

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
            QuestController.Instance.GetCurrent().SetBoolean(BuriedVaseVarName);
            var dialogController = DialogController.Instance;
            dialogController.ShowDialog(dialogController.GetDialog(VaseBuryDialog), null, () =>
            {
                StartCoroutine(useEnumerator());
            });
        });
    }

    IEnumerator useEnumerator()
    {
        base.Use();
        var c = GetComponent<GravePastController>(); var asource = GetComponent<AudioSource>();
        Controller.PlayerCharacter.GetComponent<Animator>().SetTrigger("Dig");
        asource.Play();
        yield return new WaitForSeconds(2.5f);
        c.ActionOccured();
    }
}