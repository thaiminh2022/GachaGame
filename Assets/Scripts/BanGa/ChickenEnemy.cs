using UnityEngine;
using System.Collections;

public class ChickenEnemy : MonoBehaviour, IDamageAble
{
    [Header("Chicken stat")]
    public ChickenObject chickenObject;
    private int hitTaken = 0;
    public bool isIdeling;

    [Header("Chicken Property")]
    [SerializeField] bool eggLayed;
    [SerializeField] float eggLayRate;
    [SerializeField] Transform eggLayPosition;


    private void Update()
    {
        // ! Should not be testing on Update
        // TODO: Try a better way to test this
        // TODO: Implement spawn by batches
        // ? Add a boss fight 

        if (GameState.instance.gameState == GameStates.Playing)
            OnShootEgg();

    }

    void OnShootEgg()
    {
        if (eggLayed == true) return;
        StartCoroutine(ShootEgg());
    }

    IEnumerator ShootEgg()
    {
        eggLayed = true;

        Instantiate(chickenObject.eggBullet, eggLayPosition.position, Quaternion.identity);

        yield return new WaitForSeconds(eggLayRate);
        eggLayed = false;
    }

    public void OnDead()
    {
        // TODO: add something here
    }

    public void TakeDamage()
    {
        hitTaken++;

        if (hitTaken >= chickenObject.maxHitTaken)
        {
            PlayerRocketController.instance.chickenKillCounts++;
            Destroy(gameObject);
        }

    }
}