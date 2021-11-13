using UnityEngine;

public class MoneyManager : MonoBehaviour
{

    // !Singleton
    public static MoneyManager instance;


    [Header("Money Handeler")]
    [SerializeField] private float playerMoney = 500;

    public readonly string currencyName = "freemogems";
    public readonly string currencySymbol = "^-^";

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    // Add values to money
    public void ChangeMoneyByAmmout(float ammout)
    {
        playerMoney += ammout;
    }

    // check if the money is more or enough with some value
    public bool IsMoreOrEnoughValue(float compareValue)
    {
        return playerMoney >= compareValue;
    }


    // Directly set the money
    // !not recommend to use this
    public void SetMoney(float newMoneyValue)
    {
        playerMoney = newMoneyValue;
    }
    // Get the ammout of money
    public float GetMoney()
    {
        return playerMoney;
    }


}
