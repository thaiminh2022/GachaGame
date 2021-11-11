using UnityEngine;
using System.Collections;
public class ChickenSpawner : MonoBehaviour
{
    [Header("Objects")]
    public static ChickenSpawner chickenSpawner;
    [SerializeField] float spawnTime;
    public SpawnObject[] spawnObjects;

    [Header("Spawn Logics")]
    [Tooltip("DO NOT CHANGE THIS")]
    public int index = 0; // ! DO NOT CHAHNGE THIS
    private int maxIndex;
    public SpawnStates spawnState { get; private set; }

    [Header("Chicken Batches")]
    private GameObject[] chickenBatches;
    public GameObject choosenChickenSpawnbatch = null;

    private void Awake()
    {
        if (chickenSpawner == null) chickenSpawner = this;
        else if (chickenSpawner != this) Destroy(gameObject);
    }
    private void Start()
    {
        index = 0;
        spawnState = SpawnStates.Idle;
        maxIndex = spawnObjects.Length - 1;

        chickenBatches = GameObject.FindGameObjectsWithTag("SpawnBatch");
    }
    private void Update()
    {
        // if the phase index > maxIndex mark it the end or boss battle
        if (index > maxIndex)
        {
            GameState.instance.gameState = GameStates.End;
            return;

        }

        if (HasNoEnemyLeft() && GameState.instance.gameState == GameStates.Playing)
        {
            NoEnemyLeft();
        }

        if (GameState.instance.gameState == GameStates.Spawning && index <= maxIndex && HasNoEnemyLeft())
        {
            StartSpawning();
        }
    }

    //Hany void to do checks
    private void StartSpawning()
    {
        if (spawnState == SpawnStates.Idle)
            StartCoroutine(SpawnLogic());
    }

    private IEnumerator SpawnLogic()
    {
        spawnState = SpawnStates.Spawning;
        choosenChickenSpawnbatch = ChooseRandomChickenBatch();

        for (int i = 0; i < spawnObjects[index].ammout; i++)
        {
            int rand = Random.Range(0, spawnObjects[index].posibleChickenSpawn.Length);
            Instantiate(spawnObjects[index].posibleChickenSpawn[rand], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
        }
        spawnState = SpawnStates.Finished;
        GameState.instance.gameState = GameStates.MovingChicken;

        spawnState = SpawnStates.Idle;
    }

    private bool HasNoEnemyLeft()
    {
        GameObject[] enemyleft = GameObject.FindGameObjectsWithTag("Enemy");

        return enemyleft.Length == 0;
    }
    private void NoEnemyLeft()
    {
        index++;
        GameState.instance.gameState = GameStates.Spawning;
    }
    private GameObject ChooseRandomChickenBatch()
    {
        var a = Random.Range(0, chickenBatches.Length);

        Debug.Log("Choosen chicken batch: " + chickenBatches[a].name);

        return chickenBatches[a];
    }

}

[System.Serializable]
public struct SpawnObject
{
    public string name;
    public GameObject[] posibleChickenSpawn;
    public int ammout;
}