using UnityEngine;
using System.Collections;

public class MayorSpeakController : DialogActorController
{

    private const string HasSpoken = "hasSpokenWithMayor";

    protected override string getDialog()
    {
        var cQuest = QuestController.Instance.GetCurrent();
      
        
        //pokud mám čip, mluv
        
        bool hasAlreadySpeaked;
        if (cQuest.TryGetValue(HasSpoken, out hasAlreadySpeaked) && hasAlreadySpeaked)
        {
            return "mayorSecond";
        }
        else
        {
            return "mayorFirst";
        }
        return null;
    }


    protected override void endDialogAction()
    {
        var cQuest = QuestController.Instance.GetCurrent();
        cQuest.SetValue(HasSpoken, true);
    }
}
