using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingData : MonoBehaviour
{
    public int index;

    public Building building;
    public PerlinNoise_Map map;
    public float Value;
    public float distanceParameter;

    private void Start()
    {
        ChangeSettings();
        GetComponent<Renderer>().material = building.material;
    }
    public void ChangeSettings()
    {
        map = gameObject.transform.parent.GetComponent<PerlinNoise_Map>();

        switch (index)
        {
            case 0:
                building = map.buildings[0];
                break;
            case 1:
                building = map.buildings[1];
                break;
            case 2:
                building = map.buildings[2];
                break;
            case 3:
                building = map.buildings[3];
                break;
            case 4:
                building = map.buildings[4];
                break;
            case 5:
                building = map.buildings[5];
                break;
        }

        distanceParameter = map.maxDistance - Vector2.Distance(map.centerBuildingGameObject.transform.position, transform.position);
        building.ReturnValue(building, distanceParameter * 1000f);
        Value = building.value;

        GetComponent<Renderer>().material = building.material;

    }
}
