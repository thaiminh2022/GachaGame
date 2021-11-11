using UnityEngine;

public class BulletHandeler : MonoBehaviour
{
    public float bulletSpeed;
    private Rigidbody2D rb;
    [SerializeField] private float liveTime;


    private void Start()
    {
        // Try get the Rigidbody Component on the objcet
        TryGetComponent<Rigidbody2D>(out rb);

        // Set object live time
        Destroy(gameObject, liveTime);
    }
    private void FixedUpdate()
    {
        // Move the object position depends on the rotation
        rb.MovePosition(rb.position + (Vector2)rb.transform.up * bulletSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        try
        {
            // If hit something then deal damage
            DealDamge(collider: collider);

            //! put every logic before this line: 
            // Destroy Gameobject after colliding with something
            Destroy(gameObject);
        }
        catch
        {
            // !This code only runs when there is/ was an error 
            Debug.Log("there was an error");
        }

    }

    private void DealDamge(Collider2D collider)
    {
        // Try get the IDamageAble from the collied object and set it as damageable collider
        collider.TryGetComponent<IDamageAble>(out IDamageAble damageAbleCollider);
        if (damageAbleCollider != null)
        {
            // If the "out" is not null, call the Take Damage
            damageAbleCollider.TakeDamage();
        }
    }

}