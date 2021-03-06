﻿using System.Collections;
using UnityEngine;

public class UseGoldOnVendingMachine : ItemUseOnScript
{
    public VendingMachineController controller;

    public override void Use()
    {
        if (controller.ChipObject == null)
            return;

        if (ObjectPoint != null)

            Controller.PlayerController.MoveTo(ObjectPoint.StartPoint, () =>
            {
                Controller.PlayerController.SetNewFacing(ObjectPoint.Direction);

                StartCoroutine(getChip());


            });




    }

    IEnumerator getChip()
    {
        base.Use();
        Controller.DisableInput();
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2.8f);

        controller.ChipObject.SetActive(true);

        var quest = QuestController.Instance.GetCurrent();
        quest.SetBoolean(controller.ChipWasGivenVarName);
        quest.SetBoolean(controller.ChipObject.GetComponent<InspectObjectController>().InspectedItemVariable);
        controller.ChipObject.GetComponent<PickableItem>().SetInspected();

        Controller.EnableInput();
    }
}