using UnityEngine;
using System.IO;

public class SavingManager : MonoBehaviour
{

    public static SavingManager instance;

    [Header("This section saved Obecjt")]
    private SaveObject saveObject = null;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        Load();
    }
    private void Start()
    {
        InvokeRepeating("Save", 1f, 2f);
    }

    public void Save()
    {
        var playerInventory = PlayerGachaControl.instance.GetInventory();
        var playerTotalRolls = PlayerGachaControl.instance.GetPlayerTotalRolls();
        var totalMoney = MoneyManager.instance.GetMoney();

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
        // Debug.Log(Application.persistentDataPath);
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
    private void OnApplicationQuit()
    {
        Save();
    }
}
public class SaveObject
{
    public int playerTotalRollsSave;
    public float totalMoneySave;
    public Inventory playerInventorySave;

}