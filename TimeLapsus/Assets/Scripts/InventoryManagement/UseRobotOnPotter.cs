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
        base.Use();
        R2D2.SetActive(true);

        var animator = R2D2.GetComponent<Animator>();

        animator.SetTrigger("Open");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Open"));

        yield return new WaitForSeconds(1);
        var EPSILON = 0.1f;
        var moveSpeed = 0.5f;
        var endPoint = AnimEndPoint.GetPoint(gameObject.transform.position.z).StartPoint;
        var originalPoint = R2D2.transform.position;

        foreach (var p in MoveToPoint(originalPoint, endPoint, EPSILON, moveSpeed))
            yield return p;

        yield return new WaitForSeconds(2);

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
        Hider.SetActive(false);

        QuestController.Instance.GetCurrent().SetBoolean(PotterIsDeadVarName);
        var dialogController = DialogController.Instance;
        dialogController.ShowDialog(dialogController.GetDialog(AfterPotterDeadDialog));

        R2D2.GetComponent<SpriteRenderer>().flipX = true;
        yield return new WaitForSeconds(0.5f);

        foreach (var p in MoveToPoint(endPoint, originalPoint, EPSILON, moveSpeed, false))
            yield return p;

        animator.SetTrigger("Close");
        yield return new WaitForSeconds(1);
        R2D2.SetActive(false);
        Controller.AddInventoryItem(EnumItemID.Robot);

        Destroy(gameObject);
    }

    private IEnumerable MoveToPoint(Vector3 originalPoint, Vector3 endPoint, float EPSILON, float moveSpeed, bool triggerHider = true)
    {
        var remainig = Vector3.Distance(originalPoint, endPoint);
        var direction = endPoint - R2D2.transform.position;
        var isApproaching = true;
        while (remainig >= EPSILON && isApproaching)
        {
            if(triggerHider && remainig <= 5f)
                Hider.SetActive(true);

            Debug.Log("move");
            var newPoint = R2D2.transform.position + direction * moveSpeed * Time.deltaTime;
            var newRemaining = Vector3.Distance(endPoint, newPoint);
            isApproaching = newRemaining < remainig;
            Debug.LogFormat("{0}, {1}, {2}", isApproaching, remainig, newRemaining);
            if (!isApproaching)
                break;

            R2D2.transform.position = newPoint;
            remainig = newRemaining;
            yield return null;
        }
    }
}