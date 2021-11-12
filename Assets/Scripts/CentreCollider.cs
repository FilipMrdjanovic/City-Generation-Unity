using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentreCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Building"))
        {
            string temp = other.gameObject.name;
            other.gameObject.transform.name = "Centre: " + temp;
            other.GetComponent<BuildingData>().index = 0;
            other.GetComponent<BuildingData>().ChangeSettings();
        }
    }

}

