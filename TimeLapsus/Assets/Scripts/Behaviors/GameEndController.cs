using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndController : MonoBehaviour
{

    public float OutroEnd = 28;
    // Use this for initialization
    private void Start()
    {

        Cursor.visible = false;
        if (BaseController.PreviousLoadedLevel == EnumLevel.RiverSide)
        {
            FindObjectOfType<Image>().color = Color.white;
            GetComponent<AudioSource>().Play();
        }

        StartCoroutine(end());

        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.anyKey)
        {
            clearAndGO();
        }
    }

    IEnumerator end()
    {
        yield return new WaitForSeconds(OutroEnd);
        clearAndGO();
    }

    private static void clearAndGO()
    {
        Statics.ClearAllData();
        SceneManager.LoadScene(EnumLevel.MainMenu.GetName());
    }
}