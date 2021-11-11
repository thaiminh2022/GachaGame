using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;
    public GameStates gameState = GameStates.Intro;

    private void Awake()
    {
        instance = this;
    }
}