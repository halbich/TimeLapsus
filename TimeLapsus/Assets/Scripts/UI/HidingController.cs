using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class HidingController : MonoBehaviour
{
    bool inventoryVisible = false;
    bool RightClickInitiated = false;
    public float AfterExitTime = 0;
    private Animator animComponent;

    // Use this for initialization
    private void Start()
    {
        animComponent = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RightClickInitiated = true;
        }
        else if (Input.GetMouseButtonUp(1) && RightClickInitiated)
        {
            RightClickInitiated = false;
            if (inventoryVisible) HideInventory();
            else ShowInventory();
        }
    }

    public void DebugInfo()
    {
        Debug.Log("inFOOOOOOO");
    }

    void HideInventory()
    {
        animComponent.SetTrigger("HideInventory");
        inventoryVisible = false;
    }

    void ShowInventory()
    {

        animComponent.SetTrigger("ShowInventory");
        inventoryVisible = true;
    }
}