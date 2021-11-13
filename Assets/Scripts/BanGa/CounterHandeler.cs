using UnityEngine;
using TMPro;
public class CounterHandeler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI killCouter;
    [SerializeField] TextMeshProUGUI currentPhase;


    public void UpdateKillCounter()
    {
        int killsAmmmout = PlayerRocketController.instance.chickenKillCounts;
        killCouter.text = "kills: " + killsAmmmout.ToString();
    }
    public void UpdateCurrentPhase()
    {
        string phase = ChickenSpawner.instance.spawnObjects[ChickenSpawner.instance.index].name;
        currentPhase.text = "Phase: " + phase;
    }


    public void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}