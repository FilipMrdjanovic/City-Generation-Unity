using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    GameObject map, ground;

    public float cubeSizeWidthMin = 1f;
    public float cubesizeWidthMax = 4f;

    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Buildings");
        ground = GameObject.FindGameObjectWithTag("Terrain");

        float seconds = 3f;

        //StartCoroutine(DestroyComponents(seconds, map));
        //StartCoroutine(DestroyComponents(seconds, ground));

        //StartCoroutine(BuildingTransformation(seconds, map));



    }
    IEnumerator DestroyComponents(float seconds, GameObject gameObj)
    {
        yield return new WaitForSeconds(seconds);


        foreach (Transform child in gameObj.transform)
        {
            foreach (var comp in child.GetComponents<Component>())
            {
                if (comp is Rigidbody)
                {
                    Destroy(comp);
                }
                if (comp is BoxCollider)
                {
                    Destroy(comp);
                }
            }
        }
    }

    IEnumerator BuildingTransformation(float seconds, GameObject gameObj)
    {
        yield return new WaitForSeconds(seconds);

        foreach (Transform child in gameObj.transform)
        {

            //get random Size (need to be Vector3 not Vector2) if you want to just change x scale 
            Vector3 randomSize = new Vector3(Random.Range(cubeSizeWidthMin, cubesizeWidthMax), child.transform.localScale.y, Random.Range(cubeSizeWidthMin, cubesizeWidthMax));

            //set it to the scale of previously instantiated platform 
            child.transform.localScale = randomSize;

        }
    }

}
