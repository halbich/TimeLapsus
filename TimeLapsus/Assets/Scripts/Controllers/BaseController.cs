using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseController : MonoBehaviour
{
    public GameObject InventoryItemTemplate;

    public GameObject PlayerCharacter;

    public PawnController PlayerController;

    public DescriptionController DescriptionController;

    public CursorManager CursorManager;

    public GameObject UI;

    private const float CHAR_Z_POSITION = -5f;

    public float CharacterZPosition { get { return CHAR_Z_POSITION; } }

    private static EnumLevel previousLoadedLevel;

    public static EnumLevel PreviousLoadedLevel
    {
        get { return previousLoadedLevel; }
    }

    public SceneFadeInOut Fader;

    [SerializeField]
    private List<RespawnPoint> startPositions;

    // Use this for initialization
    private void Start()
    {
        Fader = GetComponentInChildren<SceneFadeInOut>();
        CursorManager = GetComponent<CursorManager>();

        PlayerController = PlayerCharacter.GetComponent<PawnController>();
        DescriptionController = FindObjectOfType<DescriptionController>();
        if (PlayerController == null)
        {
            Debug.LogError("No controller");
            return;
        }

        var enterData = GetEnterData(PreviousLoadedLevel);
        PlayerController.SetInitPosition(enterData.StartPoint);
        PlayerController.SetNewFacing(enterData.Direction);
    }

    private void Awake()
    {
        if (UI != null)
            UI.SetActive(true);

        PlayerCharacter = GameObject.FindWithTag("Player");
        if (PlayerCharacter == null)
            return;

        var origPosition = PlayerCharacter.transform.position;
        origPosition.z = CharacterZPosition;
        PlayerCharacter.transform.position = origPosition;

        var startObjects = GameObject.FindGameObjectsWithTag("Respawn").ToList();

        startPositions = startObjects.Select(e => e.GetComponent<RespawnPointScript>().GetPoint(CharacterZPosition)).ToList();

        foreach (var startObject in startObjects)
        {
            Destroy(startObject);
        }

        var bckgrnd = GameObject.FindGameObjectWithTag("Background");
        if (bckgrnd != null)
        {
            bckgrnd.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ChangeScene(EnumLevel newLevel)
    {
        previousLoadedLevel = Statics.GetFromName(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(newLevel.GetName());
    }

    public Vector3 GetEnterPosition(EnumLevel level)
    {
        var startPoint = GetEnterData(level).StartPoint;
        return startPoint;
    }

    public RespawnPoint GetEnterData(EnumLevel level)
    {
        var result = startPositions.SingleOrDefault(e => e.LevelName == level);
        if (result != default(RespawnPoint)) // it is not default struct value
            return result;

        if (!Debug.isDebugBuild)
            return startPositions.Single(e => e.LevelName == level);

        Debug.LogWarning("nenalezen startovací objekt! " + level);
        return startPositions.First();
    }

    public void AddInventoryItem(EnumItemID toAdd)
    {
        Statics.Inventory.Add(Statics.AllInventoryItems[toAdd]);
        FindObjectOfType<InventoryController>().UpdateInventory();
    }

    public bool HasInventoryItem(EnumItemID itemID)
    {
        return Statics.Inventory.Any(e => e.ItemID == itemID);
    }

    internal void RemoveInventoryItem(EnumItemID itemID)
    {
        var item = Statics.AllInventoryItems[itemID];
        Statics.Inventory.Remove(item);
        FindObjectOfType<InventoryController>().UpdateInventory();
    }
}