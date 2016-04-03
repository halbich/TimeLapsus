using UnityEngine;
using UnityEngine.UI;

public class DialogueBlockerController :ScriptWithController
{
    private Button btn;

    protected override void Awake()
    {
        base.Awake();
        btn = GetComponent<Button>();
        btn.enabled = false;
    }

    public bool clicked;
    private bool skipOne;

    public void Activate()
    {
        gameObject.SetActive(true);
        Controller.HintController.IsHintEnabled = false;
    }

    public void WaitForClick(bool pGetMouseButtonDown)
    {
        skipOne = pGetMouseButtonDown;
        btn.enabled = true;
    }

    public void Deactivate()
    {
        btn.enabled = false;
        gameObject.SetActive(false);
        Controller.HintController.IsHintEnabled = true;
    }

    public void ButtonClicked()
    {
        if (skipOne)
        {
            skipOne = false;
            return;
        }

        clicked = true;
    }
}