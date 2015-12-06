using UnityEngine;
using System.Collections;
using Assets.Scripts.ItemManagement;
using System.Collections.Generic;

public class InventoryItemController : ClickableArea {
    public InventoryItemController()
    {
        this.cursor = CursorType.Explore;
    }
    public const float itemSpace = 0.2f;
    public const float descriptionHeight = 0.5f;
    public string ItemId { get; private set; }
    float xOffset;
    float yOffset;
    Sprite sprite;
    InventoryItem itemInfo;
    public void SetPosition(int itemIndex, int itemCount)
    {
        Bounds bounds = GetComponent<SpriteRenderer>().bounds;
        float completeSize = bounds.size.x * itemCount + itemSpace * (itemCount - 1);
        xOffset = (-completeSize / 2) + itemIndex * (bounds.size.x + itemSpace) + bounds.size.x / 2;
        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = Camera.main.aspect * camHalfHeight;
        yOffset = -camHalfHeight + bounds.size.y/2 + descriptionHeight;
        FixPosition();
    }
    public void SetItem(string itemId)
    {
        ItemId = itemId;
        if (Statics.AllInventoryItems.TryGetValue(itemId, out itemInfo))
        {
                GetComponent<SpriteRenderer>().sprite = itemInfo.ItemSprite;
                sprite = itemInfo.ItemSprite;
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        FixPosition();
    }
    void FixPosition()
    {
        Vector3 position = new Vector3(xOffset, yOffset, -Camera.main.transform.position.z - 2) + Camera.main.transform.position;

        transform.position = position;
        if (sprite != null)
        {
            var collider = GetComponent<BoxCollider2D>();
            collider.offset = sprite.bounds.center;
            collider.size = sprite.bounds.size;
        }
    }
    protected override void OnMouseEnter()
    {
        base.OnMouseEnter();
        Controller.DisplayedDescription = itemInfo.ItemName;
    }
    protected override void OnMouseExit()
    {
        base.OnMouseExit();
        Controller.DisplayedDescription = "";
    }
    private void OnMouseDown()
    {
        if (IsInBox && !Controller.DialogueActive)
        {
            Controller.DialogController.ShowMessages(new List<string> { itemInfo.ItemDescription });
        }
    }
}
