using UnityEngine;

[CreateAssetMenu(fileName = "ItemsObject", menuName = "GachaGame/ItemsObject", order = 0)]
public partial class ItemsObject : ScriptableObject {

    // ! Might change this later for better configurations
    // TODO: Add more variety (Might not)
    public new string name;
    public rollTypes rollType;
    public float percentage;
}