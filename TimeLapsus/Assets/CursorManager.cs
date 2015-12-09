using UnityEngine;
using System.Collections;



public class CursorManager : MonoBehaviour
{

    public Texture2D Main;
    public Texture2D Walk;
    public Texture2D Door;
    public Texture2D GoToLocationN;
    public Texture2D GoToLocationE;
    public Texture2D GoToLocationS;
    public Texture2D GoToLocationW;
    public Texture2D Speak;
    public Texture2D Explore;
    public Texture2D PickUp;

    public Vector2 HotSpot = Vector2.zero;
    public CursorMode CursorMode = CursorMode.Auto;

    // Use this for initialization
    void Awake()
    {
        SetCursor();
    }
   
    public void SetCursor(CursorType type = CursorType.Main)
    {
        var texture = getTexture(type);
        HotSpot = new Vector2(texture.width / 2, texture.height / 2);
        Cursor.SetCursor(texture, HotSpot, CursorMode);
    }

    private Texture2D getTexture(CursorType type)
    {
        switch (type)
        {
            case CursorType.Walk:
                return Walk;
            case CursorType.Door:
                return Door;
            case CursorType.GoToLocationN:
                return GoToLocationN;
            case CursorType.GoToLocationE:
                return GoToLocationE;
            case CursorType.GoToLocationS:
                return GoToLocationS;
            case CursorType.GoToLocationW:
                return GoToLocationW;
            case CursorType.Speak:
                return Speak;
            case CursorType.Explore:
                return Explore;
            case CursorType.PickUp:
                return PickUp;
            case CursorType.Main:
            default:
                return Main;
        }
    }
}
