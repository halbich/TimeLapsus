using System.Collections;
using UnityEngine;
using UnityEngine.VR;

public class UseRobotOnPotter : ItemUseOnScript
{
    public string PotterIsDeadVarName;
    public string AfterPotterDeadDialog;

    public GameObject R2D2;

    public ItemPointScript AnimStartPoint;
    public ItemPointScript AnimEndPoint;
    public GameObject Hider;

    public float WaitForRobot = 0.5f;

    protected override void Start()
    {
        base.Start();
        R2D2.SetActive(false);
        Hider.SetActive(false);
    }

    public override void Use()
    {
        if (AnimStartPoint == null)
        {
            Debug.LogError("No AnimStartPoint point defined!");
            return;
        }

        if (AnimEndPoint == null)
        {
            Debug.LogError("No AnimEndPoint point defined!");
            return;
        }

        var point = AnimStartPoint.GetPoint(Controller.CharacterZPosition);

        Controller.PlayerController.MoveTo(point.StartPoint, () =>
        {
            Controller.PlayerController.SetNewFacing(point.Direction);

            StartCoroutine(animate());
        });
    }

    IEnumerator animate()
    {
        //base.Use();

        Controller.PlayerCharacter.GetComponent<Animator>().SetTrigger("Throw");
        R2D2.SetActive(true);
        var animator = R2D2.GetComponent<Animator>();
        yield return new WaitForSeconds(WaitForRobot);
        animator.enabled = true;

    }


    internal void KillPotter()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        QuestController.Instance.GetCurrent().SetBoolean(PotterIsDeadVarName);
    }

    internal void ShowFinalDialog()
    {
        var dialogController = DialogController.Instance;
        dialogController.ShowDialog(dialogController.GetDialog(AfterPotterDeadDialog));

        R2D2.SetActive(false);

        Destroy(gameObject);
    }
}