using UnityEngine;

public class GameEndTrigger : ScriptWithController
{
    private void OnTriggerEnter(Collider other)
    {
        Controller.ChangeScene(EnumLevel.GameEnd);
    }
}