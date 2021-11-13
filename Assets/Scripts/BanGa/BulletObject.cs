using UnityEngine;

[CreateAssetMenu(fileName = "BulletObject", menuName = "GachaGame/BanGa/Bullet", order = 0)]
public class BulletObject : ScriptableObject
{
    public new string name;
    public BulletTypes bulletType;
    public float bulletDelay = 0.3f;

    // ?[Range(0, 5)] public int bulletLevel = 0;

    // ?public Color color;


}