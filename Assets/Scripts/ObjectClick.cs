using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SendRaycast();
        }
    }
    void SendRaycast()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.transform != null)
            {
                if (hit.transform.CompareTag("Building"))
                {
                    if (!hit.transform.GetChild(0).gameObject.activeSelf)
                        hit.transform.GetChild(0).gameObject.SetActive(true);
                    else
                        hit.transform.GetChild(0).gameObject.SetActive(false);

                }
            }
        }
    }
}
