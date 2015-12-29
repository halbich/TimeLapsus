using UnityEngine;

public class TalkingActorWithController : TalkingActor
{
    private DialogActorController dialogController;
    public Sprite Avatar;

    protected override void Start()
    {
        base.Start();

        dialogController = GetComponent<DialogActorController>();
        if (dialogController == null)
            Debug.LogErrorFormat("No dialogActorComponent defined for {0}! ", gameObject.name);
        else
            dialogController.SetAvatar(Avatar);
    }

    public void Speak()
    {
        dialogController.Speak();
    }
}