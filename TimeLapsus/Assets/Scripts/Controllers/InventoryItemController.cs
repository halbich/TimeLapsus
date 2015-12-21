using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventoryItemController : ClickableArea, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData == null)
            return;

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
}
