using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingHandeler : MonoBehaviour
{
    [SerializeField] Canvas endGameCavas;
    [SerializeField] TextMeshProUGUI currentChickenKillText;
    [SerializeField] TextMeshProUGUI currentMoneyText;


    public void SetGameState()
    {
        GameState.instance.gameState = GameStates.End;
        endGameCavas.gameObject.SetActive(false);
    }
    public void AddMoney()
    {

        MoneyManager.instance?.ChangeMoneyByAmmout(PlayerRocketController.instance.chickenKillCounts * 10);
        currentMoneyText.text = "Current Money: " + MoneyManager.instance?.GetMoney().ToString();

    }
    public void EnableCanvas()
    {
        endGameCavas.gameObject.SetActive(true);
    }

    public void RestartButton()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }
    public void BackToMain(int mainSceneIndex)
    {
        SceneManager.LoadScene(mainSceneIndex);
    }

    private void Start()
    {
        currentMoneyText.text = "Current Money: " + MoneyManager.instance?.GetMoney().ToString();
    }
    private void Update()
    {
        currentChickenKillText.text = PlayerRocketController.instance.chickenKillCounts.ToString();
    }

}