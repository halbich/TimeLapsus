using UnityEngine;
using System.Collections;

public class r2d2animation : MonoBehaviour
{



    public GameObject FightClouds;
    public UseRobotOnPotter RobotOnPotter;
    public GameObject PotterTable;

    void Start()
    {
        PotterTable.SetActive(QuestController.Instance.GetCurrent().GetBoolean(RobotOnPotter.PotterIsDeadVarName));
    }

    public void ShowClouds()
    {
        FightClouds.SetActive(true);
    }

    public void HideClouds()
    {
        PotterTable.SetActive(true);
        FightClouds.SetActive(false);
    }

    public void KillPotter()
    {
        RobotOnPotter.GetComponent<AudioSource>().Play();
        RobotOnPotter.KillPotter();
    }

    public void ShowFinalDialog()
    {
        RobotOnPotter.ShowFinalDialog();

    }

    public void StartR2Sound()
    {
        GetComponent<AudioSource>().Play();
    }
}
