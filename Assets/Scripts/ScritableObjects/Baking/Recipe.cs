
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Baking/Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField]
    public List<RecipePart> parts = new List<RecipePart>();
    public string DisplayName;
    public void CollapseParts()
    {
        Dictionary<string, int> alreadyFound = new Dictionary<string, int>();
        for(int i = 0; i < parts.Count; i++)
        {
            int index;
            if (alreadyFound.TryGetValue(parts[i].type, out index))
            {
                parts[index].amount += 1;
                parts.RemoveAt(i);
                i--;
            }
            else
            {
                alreadyFound.Add(parts[i].type, i);
            }
        }
    }
    public GameObject Result;
   
}
