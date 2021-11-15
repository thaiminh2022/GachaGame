using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private List<ItemsObject> everyFourStar = new List<ItemsObject>();
    [SerializeField] private List<ItemsObject> everyFiveStar = new List<ItemsObject>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    public void QuickDebugWithKeycode(object message, KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
        {
            Debug.Log(message);
        }
    }

    public void SwitchScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void DestroyAllChildInGameObject(Transform parentGameobject)
    {
        if (parentGameobject.childCount > 0)
        {
            for (int i = 0; i < parentGameobject.childCount; i++)
            {
                GameObject.Destroy(parentGameobject.GetChild(i).gameObject);
            }
        }
    }


    public ItemsObject GetRandomGachaFourStars()
    {
        int i = Random.Range(0, everyFourStar.Count);

        return everyFourStar[i];
    }

    public ItemsObject GetRandomGachaFiveStars()
    {
        int i = Random.Range(0, everyFiveStar.Count);

        return everyFiveStar[i];
    }

}


