using System.Collections;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    private DialogActorController dialogController;
    public float AfterTriggeredWaitPause = 2f;
    private bool dialogTriggered;

    private void Start()
    {
        dialogController = GetComponent<DialogActorController>();
        if (dialogController == null)
            Debug.LogErrorFormat("No dialogActorComponent defined for {0}! ", gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!dialogTriggered)
            StartCoroutine(trigger());
    }

    private IEnumerator trigger()
    {
        dialogTriggered = true;
        yield return new WaitForSeconds(AfterTriggeredWaitPause);
        dialogController.Speak();
    }
}