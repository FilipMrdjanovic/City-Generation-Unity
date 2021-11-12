using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCollider : MonoBehaviour
{
    public PerlinNoise_Map mapParent;
    private void Start()
    {
        mapParent = transform.parent.GetComponent<PerlinNoise_Map>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Building"))
        {
            other.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
            string temp = other.gameObject.name;
            switch (transform.name)
            {
                case "Ward 1 Main Building":
                    other.gameObject.transform.name = "Ward 1: " + temp;
                    other.GetComponent<BuildingData>().index = 1;
                    break;
                case "Ward 2 Main Building":
                    other.gameObject.transform.name = "Ward 2: " + temp;
                    other.GetComponent<BuildingData>().index = 2;
                    break;
                case "Ward 3 Main Building":
                    other.gameObject.transform.name = "Ward 3: " + temp;
                    other.GetComponent<BuildingData>().index = 3;
                    break;
                case "Ward 4 Main Building":
                    other.gameObject.transform.name = "Ward 4: " + temp;
                    other.GetComponent<BuildingData>().index = 4;
                    break;
            }
        }
    }
}
