using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SavingManager : MonoBehaviour
{

    public static SavingManager instance;

    [Header("This section saved Obecjt")]
    private SaveObject saveObject = null;
    private void Awake()
    {
        instance = this;
        Debug.Log(instance.gameObject);

        DontDestroyOnLoad(gameObject);
        Load();
    }

    private void Update()
    {
        if (SceneManager.GetSceneByName("Main").isLoaded == true)
        {
            Save();
        }
    }
    public void Save()
    {
        var playerInventory = PlayerGachaControl.instance.GetInventory();
        var playerTotalRolls = PlayerGachaControl.instance.GetPlayerTotalRolls();
        var totalMoney = MoneyManager.instance.GetMoney();

        if (playerInventory.GetItemList().Count <= 0) return;
        Debug.Log(playerInventory.GetItemList()[0]);

        // Create anew save Obejct
        SaveObject saveObjectSave = new SaveObject
        {
            playerInventorySave = playerInventory,
            playerTotalRollsSave = playerTotalRolls,
            totalMoneySave = totalMoney,
        };
        Debug.Log(saveObjectSave.playerInventorySave.GetItemList()[0]);

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
            Debug.Log(Application.persistentDataPath);
            string savedString = File.ReadAllText(Application.persistentDataPath + "/save.dontopenthis");

            // Attach the Object to current seection saveObject
            saveObject = JsonUtility.FromJson<SaveObject>(savedString);
            Debug.Log(saveObject.playerInventorySave.GetItemList()[0].itemsObject.name);

        }

    }

    public SaveObject GetSavedObject()
    {
        return saveObject;
    }
}

[System.Serializable]
public class SaveObject
{
    public int playerTotalRollsSave;
    public float totalMoneySave;
    public Inventory playerInventorySave;

}