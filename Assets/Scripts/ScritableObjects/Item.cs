using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{

    public string ItemName;
    public GameObject Model;
    public void Spawn(Vector3 pos )
    {
        Instantiate(Model, pos, Quaternion.identity);
    }
}
