using System.Collections;
using UnityEngine;

public class GameEndTrigger : ScriptWithController
{
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(nextLevel());
    }

    IEnumerator nextLevel()
    {
        yield return new WaitForSeconds(2);

        Controller.ChangeScene(EnumLevel.About);
    }
}