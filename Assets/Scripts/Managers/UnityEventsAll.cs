using UnityEngine;
using UnityEngine.Events;

public class UnityEventsAll : MonoBehaviour
{
    public static UnityEventsAll instance;

    public UnityEvent onFinishedRolling;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
}