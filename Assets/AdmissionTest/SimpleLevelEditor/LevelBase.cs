/// Jairoandrety 11/02/2021

using System.Collections.Generic;
using UnityEngine;

public class LevelBase : MonoBehaviour
{
    public static string data = string.Empty;
    public string defaultData = string.Empty; 
    public Road road;

    public int idSection = 0;
    public int idLocation = 0;
    public Vector2 currentLocation = Vector2.zero;

    //X = VerticalItems | Y = HorizontalItems
    public Vector2 sizeMap = Vector2.zero;

    public List<GameObject> roadPrefabs;
    public List<GameObject> roadPooled;

    public GameObject MapContainer;
    public MapSection MapSection;
    public List<MapSection> map;

    public GameObject tiledMap;
    public GameObject selected;

    public void SaveRoadMap()
    {
        data = JsonUtility.ToJson(road);
        Debug.Log(data);
            }

    public void LoadRoadMap()
    {
        for (int i = 0; i < sizeMap.x; i++)
        {
            for (int j = 0; j < sizeMap.y; j++)
            {
                currentLocation = new Vector2(i, j);
                idLocation = (int)(currentLocation.x * sizeMap.x + currentLocation.y);

                MapSection mapSection = Instantiate(MapSection, new Vector3(i * 25, 0, j * 25),  Quaternion.Euler(0, road.road[idLocation].rotation, 0), MapContainer.transform);
                map.Add(mapSection);
                idSection = road.road[idLocation].id;
                CreateSectionMap(i, j);

                //RoadSection section = Road.road[(int)(i * sizeMap.x + j)];
                //MapSection section = Instantiate(MapSection, new Vector3(i * 25, 0, j * 25), Quaternion.identity, MapContainer.transform);
                //Road.road.Add(new RoadSection());
            }
        }
    }

    public void CreateSectionMap(int x, int y)
    {
        map[idLocation].ClearSection();
        GameObject section = Instantiate(roadPrefabs[idSection], new Vector3(x * 25, 0, y * 25), Quaternion.Euler(0, road.road[idLocation].rotation, 0), map[idLocation].transform);
        map[idLocation].section = section;

        if (idSection == roadPooled.Count - 1)
            map[idLocation].ClearSection();
    }

    public void CreateSectionMap()
    {
        map[idLocation].ClearSection();
        GameObject section = Instantiate(roadPrefabs[idSection], selected.transform.localPosition, Quaternion.Euler(0, road.road[idLocation].rotation, 0), map[idLocation].transform);
        map[idLocation].section = section;

        if (idSection == roadPooled.Count - 1)
            map[idLocation].ClearSection();

        road.road[idLocation].SetValues(idSection, roadPooled[idSection].transform.eulerAngles.y);
    }
}
