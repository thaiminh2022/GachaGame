using UnityEngine;
using UnityEngine.Events;

public class UnityEventsAll : MonoBehaviour
{
    public static UnityEventsAll instance;

    public UnityEvent onFinishedRolling;
    //public UnityEvent onOpenInventory;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
}