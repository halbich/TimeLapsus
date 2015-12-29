using UnityEngine;

public class DialogueBlockerController : MonoBehaviour
{
    public bool clicked;

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void ButtonClicked()
    {
        clicked = true;
    }
}