public class ChangeScreen : ChangeScreenAbstract
{
    public CursorType Cursor = CursorType.GoToLocationN;

    protected override void Start()
    {
        base.Start();
        cursor = Cursor;
    }
}