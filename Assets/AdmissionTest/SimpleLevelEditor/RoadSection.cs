/// Jairoandrety 11/02/2021

using UnityEngine;

[System.Serializable]
public class RoadSection
{
    public int id = 0;
    public float rotation = 0f;

    public void SetValues(int id, float rotation)
    {
        this.id = id;
        this.rotation = rotation;
    }
}
