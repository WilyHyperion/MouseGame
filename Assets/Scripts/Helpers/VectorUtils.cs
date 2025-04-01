using UnityEngine;

public static class VectorUtils
{
    public static Vector2 ToVector2(this Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }
    public static Vector3 ToVector3(this Vector2 v, float z = 0)
    {
        return new Vector3(v.x, v.y, z);
    }
    /// <summary>
    /// Returns a vector3 with parts v.x, y or 0, v.y
    /// </summary>
    /// <param name="v"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Vector3 ToXZVector3(this Vector2 v, float y = 0)
    {
        return new Vector3(v.x, y, v.y);
    }
}
