using UnityEngine;
using System.IO;

public class SavingManager : MonoBehaviour
{

    public static SavingManager instance;

    // ? Is it worth the risk of losing data
    // [SerializeField] float updateSaveObjectDelay = .1f;
    // [SerializeField] float updateSaveDelay = .1f;

    [Header("Need to save Objects")]
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private int playerTotalRolls;
    [SerializeField] private float totalMoney;

    [Header("This section saved Obecjt")]
    private SaveObject saveObject = null;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
    private void Start()
    {
        // Load the save
        Load();
    }
    private void Update()
    {
        UpdateSaveObject();
        Save();
    }
    private void UpdateSaveObject()
    {
        if (PlayerGachaControl.instance != null)
        {
            playerInventory = PlayerGachaControl.instance.GetInventory();
            playerTotalRolls = PlayerGachaControl.instance.GetPlayerTotalRolls();
        }

        if (MoneyManager.instance != null)
            totalMoney = (float)MoneyManager.instance.GetMoney();
    }

    public void Save()
    {
        // Create anew save Obejct
        SaveObject saveObjectSave = new SaveObject
        {
            playerInventorySave = playerInventory,
            playerTotalRollsSave = playerTotalRolls,
            totalMoneySave = totalMoney,
        };

        // Embed it to a json string
        string json = JsonUtility.ToJson(saveObjectSave, true);

        // Write that json to a file. aka save.dontopenthis
        File.WriteAllText(Application.persistentDataPath + "/save.dontopenthis", json);

    }
    public void Load()
    {
        // Check if the file exist, else dont load
        if (File.Exists(Application.persistentDataPath + "/save.dontopenthis"))
        {
            string savedString = File.ReadAllText(Application.persistentDataPath + "/save.dontopenthis");

            // Attach the Object to current seection saveObject
            saveObject = JsonUtility.FromJson<SaveObject>(savedString);
        }
    }

    public SaveObject GetSavedObject()
    {
        return saveObject;
    }

    private void OnApplicationFocus(bool focusStatus)
    {
        if (focusStatus == false)
        {
            Save();
        }
    }
}
public class SaveObject
{
    public int playerTotalRollsSave;
    public float totalMoneySave;
    public Inventory playerInventorySave;

}