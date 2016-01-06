using UnityEngine;

public class ScriptWithController : MonoBehaviour
{
    protected BaseController Controller { get; private set; }

    protected virtual void Awake()
    {
        var obj = GameObject.FindWithTag("GameController");
        Controller = obj.GetComponent<BaseController>();
    }

    protected virtual void Start()
    {
    }
}