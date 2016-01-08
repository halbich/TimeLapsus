using UnityEngine;

[ExecuteInEditMode]
public class CharacterSpeedChanger : MonoBehaviour
{
    public float BigActorMoveConstant;
    public float BigActorYValue;

    public float SmallActorMoveConstant;
    public float SmallActorYValue;

    private PawnController pawnController;

    private void Awake()
    {
        pawnController = GetComponent<PawnController>();
    }

    private void Update()
    {
        var currentY = gameObject.transform.localScale.y;

        var k = (BigActorMoveConstant - SmallActorMoveConstant) / (BigActorYValue - SmallActorYValue);
        // ( );
        var speed = k * (currentY - SmallActorYValue) + SmallActorMoveConstant;

        pawnController.MoveSpeed = speed;

    }

}