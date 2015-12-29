public class QuestController
{

    private static QuestController _inst;
    public static QuestController Instance
    {
        get { return _inst ?? (_inst = new QuestController()); }
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
