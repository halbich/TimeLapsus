using UnityEngine;

public class ScriptWithController : MonoBehaviour
{
    private BaseController gameController;

    protected BaseController Controller
    {
        get
        {
            return gameController;
        }
    }

    private void Awake()
    {
        var obj = GameObject.FindWithTag("GameController");
        gameController = obj.GetComponent<BaseController>();
    }
}