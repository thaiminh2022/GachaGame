using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxSpawner : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField] Transform startBouding;
    [SerializeField] Transform endBouding;
    [SerializeField] float offset;

    [Header("Time")]
    [SerializeField] private float startTimeBetweenSpawn;
    private float timeBetweenSpawn;

    [Header("Objects")]
    [SerializeField] GameObject[] allRandomLootBox;

    private void Start()
    {
        timeBetweenSpawn = startTimeBetweenSpawn;
    }
    private void Update()
    {
        SpawnLootBox();
    }
    private void SpawnLootBox()
    {
        if (GameState.instance.gameState != GameStates.Playing) return;
        // Loot Box Spawn Logic
        if (timeBetweenSpawn <= 0)
        {
            // !Spawn a loot box
            Instantiate(GetRandomLootBox(), GetRandomPositionBetweenBouding(), Quaternion.identity);

            timeBetweenSpawn = startTimeBetweenSpawn;
        }
        else
        {
            timeBetweenSpawn -= Time.deltaTime;
        }

    }
    private Vector2 GetRandomPositionBetweenBouding()
    {
        float x1 = startBouding.position.x;
        float x2 = endBouding.position.x;

        float a = Random.Range(x1 + offset, x2 - offset);

        return new Vector2(a, startBouding.position.y);
    }

    private GameObject GetRandomLootBox()
    {
        int rand = Random.Range(0, allRandomLootBox.Length);

        return allRandomLootBox[rand];
    }

}
