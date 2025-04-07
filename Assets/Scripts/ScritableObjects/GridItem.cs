using UnityEngine;

[CreateAssetMenu(fileName = "GridItem", menuName = "Scriptable Objects/GridItem")]
public class GridItem : ScriptableObject
{
    public string Name;
    //pretty garbage name ngl but too scared to refactor
    public GameObject Placed;
}
