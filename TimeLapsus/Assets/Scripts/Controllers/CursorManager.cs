using UnityEngine;

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
    public Texture2D Use;

    public Vector2 HotSpot = Vector2.zero;
    public CursorMode CursorMode = CursorMode.Auto;
    // Use this for initialization
    private void Awake()
    {
        SetCursor();
    }

    private bool cursorChangePossible = true;

    public void FreezeCursorTexture()
    {
        cursorChangePossible = false;
    }

    public void UnfreezeCursorTexture()
    {
        cursorChangePossible = true;
    }

    public void SetCursor(CursorType type = CursorType.Main)
    {
        if (!cursorChangePossible)
            return;
        var texture = getTexture(type);
        if (texture)
        { 
        HotSpot = new Vector2(texture.width / 2f, texture.height / 2f);
        Cursor.SetCursor(texture, HotSpot, CursorMode);
        }
    }

    internal Texture2D getTexture(CursorType type)
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
            case CursorType.Use:
                return Use;
            case CursorType.None:
                return null;

            default:
                return Main;
        }
    }
}