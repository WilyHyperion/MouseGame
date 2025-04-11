using System.Collections.Generic;
using UnityEngine;

public static class ListUtils 
{
    public static T RandomChoice<T>(this List<T> list)
    {
        if(list.Count == 0)
        {
            throw new System.Exception("Tried to get choice from empty list");
        }
        return list[Random.Range(0, list.Count)];
    }
}
