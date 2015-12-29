public class MainMenuController : BaseController
{
    // Update is called once per frame
    private void Update()
    {
    }

    public void StartGame()
    {
        ChangeScene(EnumLevel.RiverSide);
    }
}