using UnityEngine;

public class GameEndController : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.anyKey)
            Application.Quit();
    }
}