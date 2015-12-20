using UnityEngine;
using System.Collections;

public class QuestController
{

    private static QuestController _inst;
    public static QuestController Instance
    {
        get
        {
            if (_inst == null)
                _inst = new QuestController();

            return _inst;
        }

    }

    private Quest currentQuest;

    private QuestController()
    {
        createQuest();
    }



    private void createQuest()
    {
        currentQuest = new Quest();
        currentQuest.CreateTestQuest();
    }

    public Quest GetCurrent()
    {
        return currentQuest;

    }
}
