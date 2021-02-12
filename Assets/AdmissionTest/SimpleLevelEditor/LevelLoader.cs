/// Jairoandrety 11/02/2021

using UnityEngine;

public class LevelLoader : LevelBase
{
    #region Behaviour
    void Start()
    {
        if (data == string.Empty)
            data = defaultData;

        road = JsonUtility.FromJson<Road>(data);
        LoadRoadMap();
    }
    #endregion
}
