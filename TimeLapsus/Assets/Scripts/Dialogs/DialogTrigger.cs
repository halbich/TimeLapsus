using System.Collections;
using UnityEngine;

public class DialogTrigger : ScriptWithController
{
    private DialogActorController dialogController;
    public float AfterTriggeredWaitPause = 2f;
    private bool dialogTriggered;
    public bool WaitForLoadCompleteTrigger = false;
    protected override void Start()
    {
        base.Start();
        dialogController = GetComponent<DialogActorController>();
        if (dialogController == null)
            Debug.LogErrorFormat("No dialogActorComponent defined for {0}! ", gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!dialogTriggered)
        {
            if (WaitForLoadCompleteTrigger)
            {
                Controller.LoadAnimationsComplete += Controller_LoadAnimationsComplete;
            }
            else StartCoroutine(trigger());
        }
    }

    private void Controller_LoadAnimationsComplete(object sender, System.EventArgs e)
    {
        StartCoroutine(trigger());
    }

    private IEnumerator trigger()
    {
        dialogTriggered = true;
        yield return new WaitForSeconds(AfterTriggeredWaitPause);
        dialogController.Speak();
    }
}