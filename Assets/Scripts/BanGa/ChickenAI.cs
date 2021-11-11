using UnityEngine;
using System.Collections.Generic;

public class ChickenAI : MonoBehaviour
{
    private ChickenSpawner chickenSpawner;
    public LayerMask spawnpointLayer;
    public float radius = 10f;

    public float chickenMoveSpeed = 5f;

    public Vector3 targetPosition = Vector3.zero;

    private void Start()
    {
        chickenSpawner = GameObject.FindGameObjectWithTag("ChickSpawner").GetComponent<ChickenSpawner>();

        GetMovingPosition();
    }
    private void Update()
    {
        MoveTowardsAttackPoint();
    }


    void MoveTowardsAttackPoint()
    {
        // Check if the spawner is in the state of spawnng
        if (chickenSpawner.spawnState == SpawnStates.Spawning)
            return;

        // Move the chicken
        GameState.instance.gameState = GameStates.MovingChicken;
        MovePosition();



    }

    private void MovePosition()
    {
        if (targetPosition != Vector3.zero)
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, chickenMoveSpeed * Time.deltaTime);
        }

        GameState.instance.gameState = GameStates.Playing;
    }

    private void GetMovingPosition()
    {

        // Create a list of position to take
        List<Transform> positionToTake = new List<Transform>();

        // Choose a random predifined chicken batch
        GameObject choosenChickenBatch = chickenSpawner.choosenChickenSpawnbatch;

        // Add all the positon to the List PositionToTake
        for (int i = 0; i < choosenChickenBatch.transform.childCount; i++)
        {
            positionToTake.Add(choosenChickenBatch.transform.GetChild(i));
        }
        for (int i = 0; i < positionToTake.Count; i++)
        {
            PositionObjectTake gameObjectIsTaken = positionToTake[i].GetComponent<PositionObjectTake>();
            if (gameObjectIsTaken.isTaken == false)
            {
                // Set the target pos to what pos
                targetPosition = positionToTake[i].position;

                // Make this gameobject a child of that target
                transform.parent = positionToTake[i];

                // Tell others gameobject that this has been taken
                gameObjectIsTaken.isTaken = true;

                break;
            }
        }
    }


    // !OnDestroy make the taken attribute to false;
    private void OnDestroy()
    {
        Transform parentTransform = transform.parent;
        parentTransform.TryGetComponent<PositionObjectTake>(out PositionObjectTake a);
        if (a != null) a.isTaken = false;
    }
}