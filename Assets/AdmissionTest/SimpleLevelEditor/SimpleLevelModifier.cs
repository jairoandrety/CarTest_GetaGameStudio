/// Jairoandrety 11/02/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLevelModifier : LevelBase
{
    #region Behaviour
    void Start()
    {
        CreateRefSections();
        CreateTiledMap();

        currentLocation = new Vector2(4, 4);
        MoveSelected(Vector2.zero);

        idSection = 1;
        CreateSectionMap();
    }

    void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            MoveSelected(new Vector2(Input.GetAxisRaw("Horizontal"), 0));
        }

        if (Input.GetButtonDown("Vertical"))
        {
            MoveSelected(new Vector2(0f, Input.GetAxisRaw("Vertical")));
        }

        if (Input.GetKeyDown("e"))
        {
            idSection += 1;

            if (idSection == roadPooled.Count)
                idSection = 1;

            ChangePooledRoad();
        }

        if (Input.GetKeyDown("q"))
        {
            idSection -= 1;

            if (idSection < 1)
                idSection = roadPooled.Count - 1;

            ChangePooledRoad();
        }

        if (Input.GetKeyDown("r"))
        {
            RotatePooledRoad();
        }

        if (Input.GetButtonDown("Jump"))
        {
            CreateSectionMap();
        }
    }
    #endregion

    #region Edit Level
    public void CreateTiledMap()
    {
        for (int i = 0; i < sizeMap.x; i++)
        {
            for (int j = 0; j < sizeMap.y; j++)
            {
                currentLocation = new Vector2(i, j);

                MapSection section = Instantiate(MapSection, new Vector3(i * 25, 0, j * 25), Quaternion.identity, MapContainer.transform);
                map.Add(section);
                road.road.Add(new RoadSection());
            }
        }
    }

    public void CreateRefSections()
    {
        for (int i = 0; i < roadPrefabs.Count; i++)
        {
            GameObject section = Instantiate(roadPrefabs[i], selected.transform.localPosition, Quaternion.identity, selected.transform);
            section.gameObject.SetActive(false);
            roadPooled.Add(section);
        }
    }

    private void MoveSelected(Vector2 direction)
    {
        currentLocation += direction;
        currentLocation = new Vector2(Mathf.Clamp(currentLocation.x, 0, sizeMap.x - 1), Mathf.Clamp(currentLocation.y, 0, sizeMap.y - 1));

        //selected.transform.Translate(new Vector3(direction.x, 0, direction.y)* 25);
        selected.transform.localPosition = new Vector3(currentLocation.x * 25, 0, currentLocation.y * 25);
        idLocation = (int)(currentLocation.x * sizeMap.x + currentLocation.y);
    }    

    private void ClearPooledRoad()
    {
        for (int i = 0; i < roadPooled.Count; i++)
        {
            roadPooled[i].SetActive(false);
        }
    }

    private void ChangePooledRoad()
    {
        map[idLocation].ClearSection();
        Debug.Log(idLocation);

        ClearPooledRoad();
        roadPooled[idSection].SetActive(true);
        road.road[idLocation].SetValues(idSection, roadPooled[idSection].transform.eulerAngles.y);
    }

    private void RotatePooledRoad()
    {
        roadPooled[idSection].transform.Rotate(Vector3.up * 90);
        road.road[idLocation].SetValues(idSection, roadPooled[idSection].transform.eulerAngles.y);
    }
    #endregion
}
