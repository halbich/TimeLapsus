using UnityEngine;
using UnityEngine.EventSystems;

public class HidingController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    private Animator animComponent;

    // Use this for initialization
    void Start()
    {
        //animComponent = GetComponentInParent<Animator>();
        //animComponent.SetTrigger("HideInventory");
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
        Debug.Log("enter");

        if (eventData == null)
            return;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit");

        if (eventData == null)
            return;

    }

}
