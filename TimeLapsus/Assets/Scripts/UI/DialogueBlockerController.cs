using UnityEngine;
using UnityEngine.UI;

public class DialogueBlockerController : MonoBehaviour
{
    private Button btn;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.enabled = false;
    }

    public bool clicked;
    private bool skipOne;

    public void Activate()
    {
        gameObject.SetActive(true);
        
    }

    public void WaitForClick(bool pGetMouseButtonDown)
    {
        Debug.Log("btn wait " + pGetMouseButtonDown);
        skipOne = pGetMouseButtonDown;
        btn.enabled = true;
    }

    public void Deactivate()
    {
        btn.enabled = false;
        gameObject.SetActive(false);
    }

    public void ButtonClicked()
    {
        if (skipOne)
        {
            skipOne = false;
            return;
        }
        

        Debug.Log("blocker clicked");
        clicked = true;
    }
}