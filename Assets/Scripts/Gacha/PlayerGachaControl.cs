using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class PlayerGachaControl : MonoBehaviour
{
    public static PlayerGachaControl instance;


    [Header("rolling items")]
    public List<ItemsObject> items;

    [Header("roll logic")]
    [SerializeField] private int totalRollCounter = 0;
    [SerializeField] private float costPerOneRoll;
    private float costPerTenRoll;

    [Header("Gotten items")]
    private List<ItemsObject> itemGotAfterRoll = new List<ItemsObject>();

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI allRolledObjectsDisplayText;
    [SerializeField] private TextMeshProUGUI playerMoneyDisplayText;

    [Header("Inventory")]
    private Inventory playerInventory;

    [Header("Others check")]
    private bool fiveStars = false;
    private bool fourStars = false;

    private GuaranteeLogicHandeler guaranteeLogicHandeler = new GuaranteeLogicHandeler();

    private ProportionalWheelSelection wheelSelection = new ProportionalWheelSelection();

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
    private void Start()
    {
        costPerTenRoll = costPerOneRoll * 10;
        LoadAllSavedObjects();

        void LoadAllSavedObjects()
        {
            // Get the save object from the manager
            SaveObject saveObject = SavingManager.instance.GetSavedObject();

            // Check if this save object is null or not
            if (saveObject != null)
            {
                // Check if is there an save inventory
                if (saveObject.playerInventorySave != null)
                {
                    playerInventory = saveObject.playerInventorySave;
                }
                else playerInventory = new Inventory();

                // Set total roll counter
                totalRollCounter = saveObject.playerTotalRollsSave;
                return;
            }
            // else:
            playerInventory = new Inventory();

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (var item in playerInventory.GetItemList())
            {
                Debug.Log(item.itemsObject.name + " : " + item.ammout);
            }
        }

        playerMoneyDisplayText.text = string.Format("{0} {1}", MoneyManager.instance.GetMoney(), MoneyManager.instance.currencyName);
    }

    public void OnRollingOneTime()
    {
        // !Guared stateman
        // If player do not have enough money to do a roll, return 
        if (MoneyManager.instance.IsMoreOrEnoughValue(costPerOneRoll) == false) return;

        itemGotAfterRoll.Clear();

        ItemsObject randomItem = wheelSelection.SelectItem(items);
        guaranteeLogicHandeler.rolledItem = randomItem;

        // Roll one-time
        if (randomItem != null)
        {
            totalRollCounter++; // Add to the counter
                                // Reduce the money ammout
            MoneyManager.instance.ChangeMoneyByAmmout(-costPerOneRoll);

            // Add to the guaranteeLogic counter
            guaranteeLogicHandeler.AddingCounters();

            // Add the gotten item to the list to display
            itemGotAfterRoll.Add(randomItem);

            // Check if we can get special stars (4, 5), if so add to the got list
            var a = GetSpecialStars();

            if (a != null) itemGotAfterRoll.Add(a);

        }
        UnityEventsAll.instance.onFinishedRolling?.Invoke();
    }
    public void OnRollingTenTimes()
    {
        // !Guared stateman
        // If player do not have enough money to do a roll, return 
        if (MoneyManager.instance.IsMoreOrEnoughValue(costPerTenRoll) == false) return;

        itemGotAfterRoll.Clear();

        // Roll ten times
        for (int i = 0; i <= 9; i++)
        {
            ItemsObject randomItem = wheelSelection.SelectItem(items);
            guaranteeLogicHandeler.rolledItem = randomItem;

            if (randomItem != null)
            {
                totalRollCounter++; // Add to the counter
                MoneyManager.instance.ChangeMoneyByAmmout(-costPerOneRoll); // Reduce the money ammout

                guaranteeLogicHandeler.AddingCounters();

                // Add the gotten item to the list to display
                itemGotAfterRoll.Add(randomItem);

                // Check if we can get special stars (4, 5), if so add to the got list
                var a = GetSpecialStars();
                if (a != null) itemGotAfterRoll.Add(a);
            }
        }
        UnityEventsAll.instance.onFinishedRolling?.Invoke();
    }

    private ItemsObject GetSpecialStars()
    {

        fourStars = guaranteeLogicHandeler.CanGetFourStars();
        fiveStars = guaranteeLogicHandeler.CanGetFiveStars();

        if (fourStars)
        {
            guaranteeLogicHandeler.ResetCounters(rollTypes.FourStars);

            return GameManager.instance.GetRandomGachaFourStars();



        }

        if (fiveStars)
        {
            guaranteeLogicHandeler.ResetCounters(rollTypes.FiveStars);

            return GameManager.instance.GetRandomGachaFiveStars();
        }

        return null;

    }

    //TODO: ADD A SPECIAL DISPLAY WHEN AN ITEM IS DUPICATE
    public void DisplayRolledItem()
    {
        allRolledObjectsDisplayText.text = "";
        foreach (var item in itemGotAfterRoll)
        {
            allRolledObjectsDisplayText.text += item.name + "\n";
        }

    }
    // Add this to events for better condition
    public void AddRolledItemToInventory()
    {
        foreach (var item in itemGotAfterRoll)
        {
            playerInventory.AddToInventory(item, ammount: 1);
        }

    }


    #region Getter and setters
    public Inventory GetInventory()
    {
        return playerInventory;
    }
    public int GetPlayerTotalRolls()
    {
        return totalRollCounter;
    }
    #endregion
}

