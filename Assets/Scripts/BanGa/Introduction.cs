using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] Transform player;
    [SerializeField] Transform[] AllMovePosition;
    [SerializeField] float rocketMoveSpeed;
    private float timeBeforeMove;
    [SerializeField] private float StartTimeBeforeMove = 5f;

    [SerializeField] private bool canMove = false;
    [SerializeField] private int index;


    [Header("Background Scrolls")]
    [SerializeField] private GameObject scrollIntroGameObject;
    [Range(-1, 1)]
    public float scrollIntroSpeed;
    private float scrollIntroOffset;
    [Range(0.00001f, Mathf.Infinity)]
    [SerializeField] private float scrollSpeedDevider;
    private Material material;

    private void Start()
    {
        material = scrollIntroGameObject.GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (GameState.instance.gameState != GameStates.Intro) return;
        if (index > AllMovePosition.Length - 1) GameState.instance.gameState = GameStates.Spawning;


        if (index == 0)
        {
            rocketMoveSpeed = 1;
        }
        else
        {
            rocketMoveSpeed = 3;
            scrollIntroSpeed = 1;
        }


        MoveTextureIntro();
        MoveIntro();

        if (canMove == true)
        {
            OnMove();
        }
    }

    private void MoveTextureIntro()
    {
        scrollIntroOffset += (Time.deltaTime * scrollIntroSpeed) / scrollSpeedDevider;
        material.SetTextureOffset("_MainTex", new Vector2(0, scrollIntroOffset));
    }
    private void MoveIntro()
    {
        if (timeBeforeMove <= 0)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
            timeBeforeMove -= Time.deltaTime;
        }
    }
    private void OnMove()
    {
        player.position = Vector2.Lerp(player.position, AllMovePosition[index].position, Time.deltaTime * rocketMoveSpeed);
        if (Vector2.Distance(player.position, AllMovePosition[index].position) < 0.03)
        {
            index++;
            timeBeforeMove = StartTimeBeforeMove;
            canMove = false;
        }

    }
}
