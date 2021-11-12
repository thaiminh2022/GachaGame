using UnityEngine.UI;
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
    [SerializeField] private Texture fightingTexture;
    [SerializeField] ParticleSystem playingParticleSystem;
    [SerializeField] Canvas blurCavas;
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
        MoveTextureIntro();

        if (GameState.instance.gameState != GameStates.Intro) return;

        if (index > AllMovePosition.Length - 1)
        {
            FinishedMoving();

            return;
        }

        if (index == 0)
        {
            rocketMoveSpeed = 1;
        }
        else if (index > 0 && index <= AllMovePosition.Length - 1)
        {
            rocketMoveSpeed = 3;
            scrollIntroSpeed = 1;
            scrollSpeedDevider = Mathf.Lerp(scrollSpeedDevider, .1f, Time.deltaTime);
            blurCavas.GetComponent<CanvasGroup>().alpha += Mathf.Clamp(Time.deltaTime / 2, 0, 1);
        }

        MoveIntro();

        if (canMove == true)
        {
            OnMove();
        }
    }

    private void FinishedMoving()
    {
        material.SetTexture("_MainTex", fightingTexture);
        scrollSpeedDevider = Mathf.Lerp(scrollSpeedDevider, 5, Time.deltaTime);
        blurCavas.GetComponent<CanvasGroup>().alpha -= Mathf.Clamp(Time.deltaTime, 0, 1);

        if (blurCavas.GetComponent<CanvasGroup>().alpha <= 0)
        {
            playingParticleSystem.Play();
            GameState.instance.gameState = GameStates.Spawning;
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
