using Assets.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemController : ClickableArea, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public static InventoryItemController DraggedObject;
    public ItemUsableArea DraggedOver;

    private InventoryItemController()
    {
        cursor = CursorType.Explore;
        IsUI = true;
    }

    public EnumItemID ItemId;
    public const float itemSpace = 10;
    public const float descriptionHeight = 0.5f;
    private float xOffset;
    private float yOffset;

    // private Sprite sprite;
    private InventoryItem itemInfo;

    private float oldXOffset;
    private float clickBeginTime;
    private Vector3 oldLocalPosition;
    private Quaternion oldLocalRotation;
    private Vector3 oldLocalScale;
    private Transform transformParent;

    public void SetPosition(int itemIndex)
    {
        var rectTransform = GetComponent<RectTransform>();
        xOffset = itemIndex * (itemSpace + rectTransform.rect.width);
        rectTransform.anchoredPosition += new Vector2(oldXOffset, 0);
        rectTransform.anchoredPosition += new Vector2(xOffset, 0);
        oldXOffset = xOffset;
    }

    public void SetItem(EnumItemID itemId)
    {
        ItemId = itemId;
        if (!Statics.AllInventoryItems.TryGetValue(itemId, out itemInfo))
            return;

        GetComponent<Image>().sprite = itemInfo.ItemSprite;
        Name = itemInfo.ItemName;
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        SetItem(ItemId);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData == null)
            return;

        OnMouseEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData == null)
            return;

        OnMouseExit();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData == null)
            return;
        var newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = -10;
        transform.position = newPos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DraggedObject = this;
        if (eventData == null)
            return;

        oldLocalPosition = transform.localPosition;
        oldLocalRotation = transform.localRotation;
        oldLocalScale = transform.localScale;
        transformParent = transform.parent;
        transform.SetParent(GetComponentInParent<Canvas>().transform, true);
        Controller.CursorManager.SetCursor();
        Controller.CursorManager.FreezeCursorTexture();
        Controller.DescriptionController.SetDescription("", false);
        Controller.DescriptionController.FreezeForItemUse();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Controller.CursorManager.UnfreezeCursorTexture();
        Controller.DescriptionController.UnfreezeForItemUse();
        Controller.DescriptionController.SetDescription("", false);
        if (eventData == null)
            return;

        transform.SetParent(transformParent, true);
        transform.localPosition = oldLocalPosition;
        transform.localRotation = oldLocalRotation;
        transform.localScale = oldLocalScale;

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            var usable = hit.collider.GetComponent<ItemUsableArea>();
            if (usable)
            {
                //    usable.Use(ItemId);
            }
        }

        if (DraggedOver != null)
            DraggedOver.Use(ItemId);

        DraggedObject = null;
        DraggedOver = null;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData == null)
            return;
        if (Time.time - clickBeginTime < 0.25f)
        {
            var di = DialogController.Instance;
            DialogController.Instance.ShowDialog(di.GetDialog(itemInfo.ItemDescription));
        }
        clickBeginTime = 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData == null)
            return;
        clickBeginTime = Time.time;
    }
}