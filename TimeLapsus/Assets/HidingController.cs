using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class HidingController : MonoBehaviour, IPointerDownHandler
{

    private Animator animComponent;

	// Use this for initialization
	void Start () {
        animComponent = GetComponentInParent<Animator>();
        animComponent.SetTrigger("HideInventory");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter()
    {
        Debug.Log("Mouse enter");
        //var info = animComponent.GetCurrentAnimatorClipInfo(0);
    }

    void OnMouseExit()
    {


    }



    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData == null)
            return;

        Debug.Log("click");
    }
}
