using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventoryItemController : ClickableArea, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public static InventoryItemController DraggedObject;
    public ItemUsableArea DraggedOver;
    InventoryItemController()
    {
        this.cursor = CursorType.Explore;
        IsUI = true;
    }
    public EnumItemID ItemId;
    public const float itemSpace = 10;
    public const float descriptionHeight = 0.5f;
    float xOffset;
    float yOffset;
    Sprite sprite;
    InventoryItem itemInfo;
    float oldXOffset = 0;
    float clickBeginTime;
    Vector3 oldLocalPosition;
    Quaternion oldLocalRotation;
    Vector3 oldLocalScale;
    Transform transformParent;


    public void SetPosition(int itemIndex)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        xOffset = itemIndex * (itemSpace + rectTransform.rect.width);
        rectTransform.anchoredPosition += new Vector2(oldXOffset, 0);
        rectTransform.anchoredPosition += new Vector2(xOffset,0);
        oldXOffset = xOffset;
    }
    public void SetItem(EnumItemID itemId)
    {
        ItemId = itemId;
        if (Statics.AllInventoryItems.TryGetValue(itemId, out itemInfo))
        {
            GetComponent<Image>().sprite = itemInfo.ItemSprite;
            sprite = itemInfo.ItemSprite;
            Name = itemInfo.ItemName;
        }
    }
    // Use this for initialization
    protected override void Start () {
        base.Start();
        SetItem(ItemId);
	}
    // Update is called once per frame
    protected void Update () {
        base.Update();
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData == null)
            return;

        base.OnMouseEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData == null)
            return;

        base.OnMouseExit();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData == null)
            return;
        transform.position = Input.mousePosition;
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
        transform.parent = transformParent;
        transform.localPosition = oldLocalPosition;
        transform.localRotation = oldLocalRotation;
        transform.localScale = oldLocalScale;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            hit.collider.GetComponent<ItemUsableArea>().Use(ItemId);
        }
        if (DraggedOver != null) DraggedOver.Use(ItemId);
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
