using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerRocketController : MonoBehaviour, IDamageAble
{
    public static PlayerRocketController instance;


    [Header("Rocket Stat")]
    [SerializeField] private Transform rocketTransform;
    [SerializeField] private float rocketSpeed;
    [SerializeField] [Range(1, 5)] private int bulletLevel;
    [SerializeField] private Transform[] spawnPositions;
    [Header("Refrence components")]
    private Rigidbody2D rb;
    private Vector3 touchPosition;
    private Touch touch;
    public UnityEvent OnTouchingTheScreen;
    [Header("Rocket Current Stat")]
    public BulletObject bulletObject;
    [SerializeField] GameObject spreadBullet;
    [SerializeField] GameObject autoBullet;

    private bool bulletAlreadySpawn = false;

    // TODO: [SerializeField] LineRenderer lazerBullet;
    public float chickenKillCounts;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void Start()
    {
        // Try get the Rigidbody, output it into rb
        TryGetComponent<Rigidbody2D>(out rb);

        // Set the default bullet obejct
        bulletObject = (BulletObject)Resources.Load("BulletType/SpreadBullet");
    }

    private void Update()
    {
        if (GameState.instance.gameState != GameStates.Playing) return;

        bulletLevel = bulletLevel > 5 ? 5 : bulletLevel;

        // If the phone touch count > 0, Invoke Event
        if (Input.touchCount > 0)
        {
            OnTouchingTheScreen?.Invoke();
        }

        // ! Only for testing, comments this line out before build
        //ShootBullet();
    }

    // Move the ship bt touching using Transform with lerp for smoothness
    // ? This method is debatable with using Physics
    public void MoveObjectByTouchHandeler()
    {
        touch = Input.GetTouch(0);
        touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        touchPosition.z = 0;

        rocketTransform.position = Vector2.Lerp(rocketTransform.position, touchPosition, Time.deltaTime * rocketSpeed);

    }
    // Shoot Handeler
    public void ShootBullet()
    {
        //  !Guard statemen
        if (bulletObject == null) return;
        if (bulletAlreadySpawn == true) return;


        // Check for the bullet type
        switch (bulletObject.bulletType)
        {
            case BulletTypes.Spread:
                StartCoroutine(BulletSpread());
                break;
            case BulletTypes.LazerAuto:
                break;
            case BulletTypes.Auto:
                StartCoroutine(BulletAuto());
                break;
            case BulletTypes.LazerTarget:
                break;
        }



    }

    #region BulletChoosingMethod
    private IEnumerator BulletSpread()
    {
        bulletAlreadySpawn = true;

        // Five next lines of code corespond to bullet rotation on spread
        int left = 20;
        int hleft = 10;
        int middle = 0;
        int hright = -10;
        int right = -20;

        // Create an array with numbers above for better usage
        int[] positionArray = { left, hleft, middle, hright, right };


        // ! case 4 is the same as case 3. Unity 2020.x do not support c#9 cool stuff with switch.
        switch (bulletLevel)
        {
            case 1:
                Instantiate(spreadBullet, spawnPositions[2].position, Quaternion.Euler(0, 0, positionArray[2]));

                break;
            case 2:
                Instantiate(spreadBullet, spawnPositions[1].position, Quaternion.Euler(0, 0, positionArray[1]));
                Instantiate(spreadBullet, spawnPositions[3].position, Quaternion.Euler(0, 0, positionArray[3]));
                break;
            case 3:
                for (int i = 1; i < 4; i++)
                {
                    Instantiate(spreadBullet, spawnPositions[i].position, Quaternion.Euler(0, 0, positionArray[i]));
                }
                break;

            case 4:
                for (int i = 1; i < 4; i++)
                {
                    Instantiate(spreadBullet, spawnPositions[i].position, Quaternion.Euler(0, 0, positionArray[i]));
                }
                break;

            case 5:
                for (int i = 0; i < spawnPositions.Length; i++)
                {
                    Instantiate(spreadBullet, spawnPositions[i].position, Quaternion.Euler(0, 0, positionArray[i]));
                }

                break;
            default:
                break;
        }
        yield return new WaitForSeconds(bulletObject.bulletSpeed);
        bulletAlreadySpawn = false;
    }
    private IEnumerator BulletAuto()
    {
        bulletAlreadySpawn = true;

        // ! case 4 is the same as case 3. Unity 2020.x do not support c#9 cool stuff with switch.
        switch (bulletLevel)
        {
            case 1:
                Instantiate(autoBullet, spawnPositions[2].position, Quaternion.identity);
                break;
            case 2:
                Instantiate(autoBullet, spawnPositions[1].position, Quaternion.identity);
                Instantiate(autoBullet, spawnPositions[3].position, Quaternion.identity);
                break;
            case 3:
                for (int i = 1; i < 4; i++)
                {
                    Instantiate(autoBullet, spawnPositions[i].position, Quaternion.identity);
                }
                break;
            case 4:
                for (int i = 1; i < 4; i++)
                {
                    Instantiate(autoBullet, spawnPositions[i].position, Quaternion.identity);
                }
                break;
            case 5:
                for (int i = 0; i < spawnPositions.Length; i++)
                {
                    Instantiate(autoBullet, spawnPositions[i].position, Quaternion.identity);
                }
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(bulletObject.bulletSpeed);
        bulletAlreadySpawn = false;
    }
    #endregion

    public void TakeDamage()
    {
        Debug.Log("Ouch!");
        Debug.Log("Im ded");
    }

    public void OnDead()
    {
        // TODO: add something
    }
}