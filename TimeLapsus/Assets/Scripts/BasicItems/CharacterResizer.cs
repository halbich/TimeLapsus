using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class CharacterResizer : MonoBehaviour
{

    private CharacterResizeController controller;
    


    // Use this for initialization
    private void Start()
    {
        controller = FindObjectOfType<CharacterResizeController>();

        if (controller != null)
            return;

        Debug.LogError("No characterResizeController defined in scene!");
    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null )
        {
            Debug.LogError("No characterResizeController defined in scene!");
            return;
        }

        transform.localScale = controller.GetScale(transform.position);
    }


   

}
