using UnityEngine;

public abstract class DialogActorController : MonoBehaviour
{

    private Sprite avatar;

    public void SetAvatar(Sprite Avatar)
    {
        avatar = Avatar;
    }

    public void Speak()
    {
        var di = DialogController.Instance;
        var dialog = di.GetDialog(getDialog());

        if (di != null)
            di.ShowDialog(dialog, avatar, endDialogAction);

    }

    protected abstract string getDialog();

    protected virtual void endDialogAction()
    {

    }
}
