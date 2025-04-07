using System.Collections.Generic;
using UnityEngine;

public abstract class Station : MonoBehaviour
{
    public (int, int) GridIndex;
    public Vector3 InteractionPoint;
    public bool InUse;
    public List<Task> NeededWork = new List<Task>();
}
