using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomObjects : MonoBehaviour
{
    private void Awake()
    {
        PerlinNoise_Map ground = GetComponent<PerlinNoise_Map>();

        GameObject centre = ground.centerGameObject;

     }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Building"))
        {
            other.GetComponent<Building>().city_Quartery = City_Quarter.Centre;
        }
    }
}
