using UnityEngine;
using System.Collections;

public class EasterEgg : ScriptWithController
{
    public EnumItemID ItemToObtain;

    private string[] sequence = new string[] { "Y:1", "Y:1", "Y:-1", "Y:-1", "X:-1", "X:1", "X:-1", "X:1", "Back", "Submit" };
    private int currentExpected = 0;

    private string originalX;
    private string originalY;
    private string originalSubmit;
    private string originalBack;
    private string toWrite = string.Empty;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        originalX = string.Empty;
        originalY = string.Empty;
        originalSubmit = string.Empty;
        originalBack = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        toWrite = string.Empty;
        var newX = string.Empty;

        var ax = Input.GetAxis("Horizontal");
        if (ax == 1 || ax == -1)
        {
            newX = "X:" + ax.ToString();
        }

        if (originalX != newX)
        {
            if (originalX != string.Empty)
                toWrite = originalX;

            originalX = newX;
        }

        var newY = string.Empty;
        var ay = Input.GetAxis("Vertical");
        if (ay == 1 || ay == -1)
        {
            newY = "Y:" + ay.ToString();
        }

        if (originalY != newY)
        {
            if (originalY != string.Empty)
                toWrite = originalY;

            originalY = newY;
        }

        var newSubmit = string.Empty;
        var aSubmit = Input.GetAxis("Submit");
        if (aSubmit == 1 || aSubmit == -1)
        {
            newSubmit = "Submit";
        }

        if (originalSubmit != newSubmit)
        {
            if (originalSubmit != string.Empty)
                toWrite = originalSubmit;

            originalSubmit = newSubmit;
        }

        var newBack = string.Empty;
        var aBack = Input.GetAxis("Cancel");
        if (aBack == 1 || aBack == -1)
        {
            newBack = "Back";
        }

        if (originalBack != newBack)
        {
            if (originalBack != string.Empty)
                toWrite = originalBack;

            originalBack = newBack;
        }

        if (toWrite != string.Empty)
        {
            if (toWrite == sequence[currentExpected])
                currentExpected++;
            else
                currentExpected = 0;

            if (currentExpected == sequence.Length)
            {
                if (!Controller.HasInventoryItem(ItemToObtain))
                    Controller.AddInventoryItem(ItemToObtain);
                currentExpected = 0;
            }

        }
    }


}
