using UnityEngine;
using System.Collections;

public class DialogueBlockerController : MonoBehaviour {

    public bool clicked = false;
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
