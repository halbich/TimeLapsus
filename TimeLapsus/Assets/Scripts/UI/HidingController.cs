using UnityEngine;
using UnityEngine.EventSystems;

public class HidingController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animComponent;

    // Use this for initialization
    private void Start()
    {
        animComponent = GetComponentInParent<Animator>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData == null)
            return;

        Debug.Log("click");
    }

    public void DebugInfo()
    {
        Debug.Log("inFOOOOOOO");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       // Debug.Log("enter");
        if (eventData == null)
            return;
        animComponent.SetTrigger("ShowInventory");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       // Debug.Log("exit");

        if (eventData == null)
            return;
        animComponent.SetTrigger("HideInventory");
    }
}