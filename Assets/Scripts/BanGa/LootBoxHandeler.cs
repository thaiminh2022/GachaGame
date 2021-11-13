using UnityEngine;

public class LootBoxHandeler : MonoBehaviour
{

    public BulletTypes lootBoxRepresentType;

    [SerializeField] float lootBoxFallSpeedMin;
    [SerializeField] float lootBoxFallSpeedMax;

    [SerializeField] float liveTime;

    private float fallSpeed;
    Rigidbody2D rb;
    private void Start()
    {
        fallSpeed = GetRandomFallSpeed();
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, liveTime);
    }

    private void Update()
    {
        if (GameState.instance.gameState != GameStates.Playing) return;
        MoveHandel();
    }
    private float GetRandomFallSpeed()
    {
        return Random.Range(lootBoxFallSpeedMin, lootBoxFallSpeedMax);
    }
    private void MoveHandel()
    {
        rb.MovePosition((Vector3)rb.position - rb.transform.up * fallSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") == true)
        {
            Debug.Log(collider.name);
            PlayerRocketController.instance.TouchALootBox(lootBoxRepresentType);
            Destroy(gameObject);
        }


    }
}