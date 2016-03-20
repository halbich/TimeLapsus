using UnityEngine;

public class ClickableArea : ScriptWithController
{
    protected CursorType cursor = CursorType.Main;
    public bool ShowHints = true;
    //HACK: Unity editor does not support nullable - we have to recognize not set position somehow.
    protected override void Start()
    {
        base.Start();
        if (ShowHints)
        {
            Transform hintPositionTransform = null;
            var areaCollider = GetComponent<Collider2D>();
            //  if (areaCollider != null)
            // {
            foreach (var child in GetComponentsInChildren<Transform>())
            {
                if (child.CompareTag("HintPosition"))
                {
                    hintPositionTransform = child;
                    break;
                }
            }
            var hintObject = Instantiate(Controller.HintController.HintTemplate);
            if (hintPositionTransform != null)
            {
                hintObject.transform.Translate(hintPositionTransform.position);
                hintObject.transform.Translate(new Vector3(0, 0, -8 - hintObject.transform.position.z));

            }
            else if (areaCollider != null)
            {
                var position = areaCollider.bounds.center;
                hintObject.transform.Translate(position);
                hintObject.transform.SetParent(areaCollider.transform, true);
                hintObject.transform.Translate(new Vector3(0, 0, -4));
            }
            var hintController = hintObject.GetComponent<ClickableAreaHint>();
            hintController.Parent = this;
            //    }
        }
    }

    protected bool IsInBox;

    [Tooltip("This name will ber visible as description")]
    public string Name;

    protected bool IsUI;

    protected bool IsOverUI()
    {
        return !IsUI && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }

    protected void Update()
    {
        if (!(Controller && Controller.CursorManager)) return;
        if (!IsInBox || IsOverUI() || !enabled)
            return;

        Controller.CursorManager.SetCursor(cursor);
        Controller.DescriptionController.SetDescription(Name, false);
    }

    protected void OnMouseEnter()
    {
        if (!enabled)
            return;

        IsInBox = true;
    }

    protected void OnMouseExit()
    {
        if (!enabled)
            return;

        IsInBox = false;
        if (!(Controller && Controller.CursorManager)) return;
        Controller.CursorManager.SetCursor();
        Controller.DescriptionController.SetDescription("", false);
    }

    public CursorType GetCurrentCursor()
    {
        return cursor;
    }

    void AddHintSprite()
    {

    }

}