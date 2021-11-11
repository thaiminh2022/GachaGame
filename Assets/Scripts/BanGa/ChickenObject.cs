using UnityEngine;

[CreateAssetMenu(fileName = "ChickenObject", menuName = "GachaGame/BanGa/ChickenObject", order = 0)]
public class ChickenObject : ScriptableObject
{
    public new string name;
    public int maxHitTaken = 3;

    public ChickenTypes chickenType;

    public GameObject eggBullet;

}