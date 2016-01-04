using UnityEngine;
using System.Collections;

public class GameEndTrigger : ScriptWithController
{


    private void OnTriggerEnter(Collider other)
    {
        Controller.ChangeScene(EnumLevel.GameEnd);
    }


}
