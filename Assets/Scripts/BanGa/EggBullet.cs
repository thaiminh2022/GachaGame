using UnityEngine;

public class EggBullet : MonoBehaviour
{
    public float eggSpeed;
    private Rigidbody2D rb;
    [SerializeField] private float liveTime;

    // ! EVERY NOTES HERE IS THE SAME CODE "BulletHandeler.cs", ANY OTHER COMMENTS ARE ADDITIONS

    private void Start()
    {
        TryGetComponent<Rigidbody2D>(out rb);
        Destroy(gameObject, liveTime);
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (Vector2)(-rb.transform.up) * eggSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        try
        {
            DealDamge(collider: collider);

            //! put every logic before this line: 
            Destroy(gameObject);
        }
        catch
        {
            Debug.Log("there was an error");
        }

    }

    private void DealDamge(Collider2D collider)
    {
        collider.TryGetComponent<IDamageAble>(out IDamageAble damageAbleCollider);
        if (damageAbleCollider != null)
        {
            damageAbleCollider.TakeDamage();
        }
    }


}