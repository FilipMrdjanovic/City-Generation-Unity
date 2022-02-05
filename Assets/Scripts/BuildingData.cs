using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

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
        StartCoroutine(setCanvasValue());
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
            case 6:
                building = map.buildings[6];
                break;
        }

        distanceParameter = map.maxDistance - Vector2.Distance(map.centerBuildingGameObject.transform.position, transform.position);
        building.ReturnValue(building, distanceParameter * 1000f);
        Value = building.Value;

        distanceParameter = float.Parse(string.Format("{0:0.00}", distanceParameter));

        GetComponent<Renderer>().material = building.material;


    }
    

    IEnumerator setCanvasValue()
    {
        yield return new WaitForSeconds(5);


        GameObject canvas = Instantiate(map.Canvas, map.transform);
        canvas.transform.SetParent(transform);
       
        GameObject parent = canvas.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;

        //PropertyInfo[] properties = typeof(Building).GetProperties();

        var attributeCount = building.GetType().GetFields().Where(i => i.IsPublic);

        attributeCount = attributeCount.Skip(1);
        attributeCount = attributeCount.Take(attributeCount.Count() - 1);
        
        foreach (FieldInfo item in attributeCount)
        {
            Text textObj = Instantiate(map.text, parent.transform);
            if(item.Name == "Value")
                textObj.text = item.Name.ToString() + ": " + Value + "$";
            else if (item.Name == "Parking")
                textObj.text = item.Name.ToString() + ": " + ((bool)item.GetValue(building) ? "Included" : "Not Included");
            else if (item.Name == "Duplex")
                textObj.text = item.Name.ToString() + ": " + ((bool)item.GetValue(building) ? "Is Duplex" : "Is Not Duplex");
            else if (item.Name == "Terrace")
                textObj.text = item.Name.ToString() + ": " + ((bool)item.GetValue(building) ? "Included" : "Not Included");
            else if (item.Name == "Size")
                textObj.text = item.Name.ToString() + ": " + item.GetValue(building) + " sqm";
            else
                textObj.text = item.Name.ToString() + ": " + item.GetValue(building);
        }
        //canvas.GetComponent<UnityEngine.UI.Text>().text = "Price : " + Value.ToString() + "$";
        //canvas.transform.Find("Text").GetComponent<Text>().text = Value.ToString();
        canvas.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        canvas.transform.GetChild(0).transform.position = new Vector3(transform.position.x, map.noiseHeight + 10f, transform.position.z);
        canvas.SetActive(false);

        //text = canvas.transform.Find("Text").GetComponent<Text>();
        //SetText(Value);

    }
}
//5.995721/2 - 3.932428/2