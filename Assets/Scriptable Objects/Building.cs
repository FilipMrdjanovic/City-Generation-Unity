using System;
using UnityEngine;


public enum City_Quarter { Ward_1, Ward_2, Centre, Ward_3, Ward_4, Rural};
public enum Noise { Low, Medium, High };
public enum Quality { Low, Medium, High };
public enum Isolation { Low, Medium, High };
public enum View { Bad, Medium, Good };


[CreateAssetMenu(menuName ="Scriptable Objects/Building")]
public class Building : ScriptableObject
{
    public City_Quarter city_Quartery;          // Included
    public Noise Noise;                         // Included
    public Quality Quality;                     // Included
    public Isolation Isolation;                 // Included
    public View View;                           // Included
    public bool Parking;                        // Included
    public float Size;                          // Calculating
    public bool Duplex;                         // Included
    public int Rooms;                           // Included
    public int Toalets;                         // Included
    public bool Terrace;                        // Included
    public float Value;                         // Calculating
    public Material material;                   // Included


    public void ReturnValue(Building building, float distance)
    {
        float noiseParameter = 0, qualityParameter = 0, isolationParameter = 0, viewParameter = 0,roomParameter = 0, toaletParameter = 0;
        float valuePerSquare;
        switch (building.city_Quartery)
        {
            case City_Quarter.Centre:
                valuePerSquare = 2000;
                break;
            case City_Quarter.Ward_1:
                valuePerSquare = 1500;
                break;
            case City_Quarter.Ward_2:
                valuePerSquare = 1700;
                break;
            case City_Quarter.Ward_3:
                valuePerSquare = 1900;
                break;
            case City_Quarter.Ward_4:
                valuePerSquare = 1600;
                break;
            case City_Quarter.Rural:
                valuePerSquare = 1200;
                break;
            default:
                valuePerSquare = 1100;
                break;
        }

        switch (building.Noise)
        {
            case Noise.Low:
                noiseParameter = 1.3f;
                break;
            case Noise.Medium:
                noiseParameter = 1.2f;
                break;
            case Noise.High:
                noiseParameter = 1;
                break;
        }

        switch (building.Quality)
        {
            case Quality.High:
                qualityParameter = 1.3f;
                break;
            case Quality.Medium:
                qualityParameter = 1.2f;
                break;
            case Quality.Low:
                qualityParameter = 1;
                break;
        }

        switch (building.Isolation)
        {
            case Isolation.High:
                isolationParameter = 1.3f;
                break;
            case Isolation.Medium:
                isolationParameter = 1.2f;
                break;
            case Isolation.Low:
                isolationParameter = 1;
                break;
        }

        switch (building.View)
        {
            case View.Good:
                viewParameter = 1.3f;
                break;
            case View.Medium:
                viewParameter = 1.2f;
                break;
            case View.Bad:
                viewParameter = 1;
                break;
        }

        float parkingParamter = building.Parking ? 3000 : 0;
        float duplexParameter = building.Duplex ? 3000 : 0;
        float terraceameter = building.Terrace ? 1500 : 0;
        for (int i = 0; i < building.Rooms; i++)
        {
            roomParameter += 500;
        }
        for (int i = 0; i < building.Toalets; i++)
        {
            toaletParameter += 500;
        }
        building.Value = Size * valuePerSquare * noiseParameter * qualityParameter * isolationParameter * viewParameter 
            + parkingParamter + duplexParameter + roomParameter + toaletParameter + terraceameter + distance;

        building.Value = ((int)Math.Ceiling(building.Value / 100.0)) * 100; // Rounds to nearest number devideable by 100
    }

}
