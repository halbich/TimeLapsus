using UnityEngine;
using System.Collections;

public class ClickableAreaHint : ScriptWithController {

    public ClickableArea Parent;
    SpriteRenderer hintImage;
    CursorType? lastCursorType = null;
	// Use this for initialization
	protected override void Start () {
        base.Start();
        hintImage = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        hintImage.enabled = Controller.HintController.IsHintActive && Parent.isActiveAndEnabled;
        if (!lastCursorType.HasValue || lastCursorType.Value != Parent.GetCurrentCursor())
        {
            UpdateImage();
        }
	}

    void UpdateImage()
    {
        lastCursorType = Parent.GetCurrentCursor();
        var textureToSet = Controller.CursorManager.getTexture(lastCursorType.Value);
        hintImage.sprite =  Sprite.Create(textureToSet, new Rect(0, 0, textureToSet.width, textureToSet.height), new Vector2(0.5f, 0.5f));
    }
}
