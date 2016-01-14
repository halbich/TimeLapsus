using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class HidingController : MonoBehaviour
{
    bool inputDisabled = false;
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
            if (!inputDisabled)
            {
                if (inventoryVisible) HideInventory();
                else ShowInventory();
            }
        }
    }

    public void DebugInfo()
    {
        Debug.Log("inFOOOOOOO");
    }

    void HideInventory()
    {
        if (animComponent.GetBool("ShowInventory") || animComponent.GetBool("HideInventory")) return;
        animComponent.SetTrigger("HideInventory");
        inventoryVisible = false;
    }

    void ShowInventory()
    {
        if (animComponent.GetBool("ShowInventory") || animComponent.GetBool("HideInventory")) return;
        animComponent.SetTrigger("ShowInventory");
        inventoryVisible = true;
    }
    public void DisableInput()
    {
        inputDisabled = true;
        if (inventoryVisible)
        {
            HideInventory();
        }
    }
    public void EnableInput()
    {
        inputDisabled = false;
    }
}