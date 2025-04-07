using UnityEngine;

public enum TaskType{
    /// <summary>
    /// Spend time at the station 
    /// </summary>
    Work,
    /// <summary>
    /// Bring items to the station
    /// </summary>
    Fetch
}
/// <summary>
/// Some work to be done at a station
/// Probably should have split these fields into different subclasses, oh well
/// </summary>
public struct Task
{
    public TaskType Type;
    public float WorkAmount;
    public float CurrentWork;
    public string FetchType;
    public int FetchAmount;
    public int CurrentlyFetched;
}
