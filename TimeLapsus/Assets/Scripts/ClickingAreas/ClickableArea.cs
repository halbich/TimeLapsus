using UnityEngine;
using System.Collections;

public class ClickableArea : MonoBehaviour {

    private PolygonCollider2D polCol;

    private BaseController gameController;

    protected BaseController Controller
    {
        get
        {
            return gameController;
            
        }
    }

    protected bool IsInBox = false;

	// Use this for initialization
	void Start ()
	{
	   
	}


    void Awake()
    {
        polCol = gameObject.GetComponent<PolygonCollider2D>();
        var obj = GameObject.FindWithTag("GameController");
        gameController = obj.GetComponent<BaseController>();
    }

    void OnMouseEnter()
    {

        IsInBox = true;
    }

    void OnMouseExit()
    {
        IsInBox = false;

    }
}
