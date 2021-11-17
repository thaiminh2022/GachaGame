using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private TextMeshProUGUI playerMoneyDisplayText;

    [Header("Display rolled Ui")]

    [SerializeField] Color FourStarsColor;
    [SerializeField] Color FiveStarsColor;
    [SerializeField] Color TwoStarsColor;
    [SerializeField] GameObject displayRolledTemplate;
    [SerializeField] GameObject displayRolledCt;
    [SerializeField] float startTimeBeforeSpawnDisplay = .5f;

    [Header("Ui On clikc")]
    [SerializeField] private GameObject inventoryItemTemplate;

    [SerializeField] private GameObject inventoryDisplayUi;
    [SerializeField] private GameObject inventoryDisplayCt;

    [Header("Description Ui")]
    [SerializeField] TextMeshProUGUI descriptionTitleText;
    [SerializeField] TextMeshProUGUI descriptionText;

    [Header("Play Ui")]
    [SerializeField] GameObject playDisplayUi;

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

    }
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

    #region Rolling
    public void OnRollingOneTime()
    {
        // !Guared stateman
        if (MoneyManager.instance.IsMoreOrEnoughValue(costPerOneRoll) == false) return;


        // Clear the item after rolls for more item
        itemGotAfterRoll.Clear();

        // Get the random item and pass to the logic handeler
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

    #endregion

    #region Inventory


    public void OnOpenInventory()
    {

        inventoryDisplayUi.SetActive(true);
        descriptionTitleText.text = "";
        descriptionText.text = "";

        var itemList = playerInventory.GetItemList();

        if (itemList.Count > 0 && inventoryDisplayUi.activeSelf == true)
        {
            //Destroy if there's already exist any objects
            GameManager.instance.DestroyAllChildInGameObject(inventoryDisplayCt.transform);
            foreach (var item in itemList)
            {
                // Create and set the parent
                GameObject go = Instantiate(inventoryItemTemplate, Vector3.zero, Quaternion.identity);
                go.transform.SetParent(inventoryDisplayCt.transform, false);

                // Change the image type
                go.transform.GetChild(0).GetComponent<Image>().sprite = item.itemsObject.representSprite;
                go.GetComponentInChildren<TextMeshProUGUI>().text = item.ammout.ToString();

                go.GetComponent<Button>().onClick.AddListener(() => ChangeDescription(item.itemsObject));
            }
        }
    }
    public void OnOpenPlayMenu()
    {
        playDisplayUi.SetActive(true);
    }

    public void TurnOffInventory()
    {
        inventoryDisplayUi.SetActive(false);
    }
    public void TurnOffPlayMenu()
    {
        playDisplayUi.SetActive(false);
    }
    private void ChangeDescription(ItemsObject itemObject)
    {
        descriptionTitleText.text = string.Format("{0} - {1}", itemObject.name, itemObject.rollType.ToString());
        descriptionText.text = itemObject.description;
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

    public void AddRolledItemToInventory()
    {
        foreach (var item in itemGotAfterRoll)
        {
            playerInventory.AddToInventory(item, ammount: 1);
        }

    }

    #endregion

    #region Display Rolling Item

    // Handel item display
    public void DisplayRolledItem()
    {

        StartCoroutine(displayRolledItemLogic());

    }
    IEnumerator displayRolledItemLogic()
    {
        GameManager.instance.DestroyAllChildInGameObject(displayRolledCt.transform);

        foreach (var item in itemGotAfterRoll)
        {
            RolledObjectTemplate goTP = Instantiate(displayRolledTemplate, Vector3.zero, Quaternion.identity).GetComponent<RolledObjectTemplate>();

            goTP.transform.SetParent(displayRolledCt.transform, false);

            goTP.spriteDisplay.sprite = item.representSprite;
            goTP.textDisplay.text = item.name;

            switch (item.rollType)
            {
                case rollTypes.FiveStars:
                    goTP.ButtonDisply.GetComponent<Image>().color = FiveStarsColor;
                    break;
                case rollTypes.FourStars:
                    goTP.ButtonDisply.GetComponent<Image>().color = FourStarsColor;
                    break;
                case rollTypes.TwoStars:
                    goTP.ButtonDisply.GetComponent<Image>().color = TwoStarsColor;
                    break;
                default:
                    break;

            }


            goTP.ButtonDisply.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnOpenInventory();
                ChangeDescription(item);
            });

            yield return new WaitForSeconds(startTimeBeforeSpawnDisplay);

        }

    }
    #endregion

    // Add this to events for better condition

    // private void OnApplicationFocus(bool focusStatus)
    // {
    //     if (focusStatus == false) SavingManager.instance.Save();
    // }
    private void OnApplicationQuit()
    {
        SavingManager.instance.Save();
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

