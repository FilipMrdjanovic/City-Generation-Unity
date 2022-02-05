using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerlinNoise_Map : MonoBehaviour
{
    public enum ObjType { Ground , Building }

    [SerializeField]
    public ObjType blockType;

    public GameObject blockGameObject;
    public GameObject Canvas;
    public Text text;

    public Building[] buildings;

    public float worldSizeX = 10;
    public float worldSizeZ = 10;

    public float noiseHeight = 3;

    public float gridOffset = 2;

    public float objectHeightOffset = 10;

    public float actualSize;

    public float gameObjectScale;

    public GameObject centerGameObject, centerBuildingGameObject;

    public float randomizeHeight = 3f;

    public float scale;

    public Building[] buildingsCopied;

    public float maxDistance;

    public GameObject perlinNoiseObject;

    public Material mat;

    private void Start()
    {
        PerlinNoise_Map map = GameObject.FindGameObjectWithTag("Buildings").GetComponent<PerlinNoise_Map>();
        PerlinNoise_Map ground = GameObject.FindGameObjectWithTag("Terrain").GetComponent<PerlinNoise_Map>();

        buildingsCopied = new Building[buildings.Length];
        for (int i = 0; i < buildingsCopied.Length; i++)
        {
            buildingsCopied[i] = buildings[i];
        }

        if (blockType == ObjType.Ground)
        {
            blockGameObject.transform.localScale = new Vector3(gameObjectScale, gameObjectScale, gameObjectScale);
            gridOffset += gameObjectScale - gridOffset;
        }

        float offsetX = 0;
        float offsetY = 0;
        float offsetZ = 0;

        int count = 0;
        int buildingCount = 0;


        float quarter1 = (worldSizeX * worldSizeZ) * 0.25f;
        float quarter4 = (worldSizeX * worldSizeZ) * 0.75f;
        float quarter2 = quarter1 + worldSizeX / 2;
        float quarter3 = quarter4 - worldSizeX / 2;


        scale = worldSizeX / 5f;

        actualSize = worldSizeX * gridOffset;

        Vector3 maxScale;
        for (int x = 0; x < worldSizeX; x++)
        {
            for (int z = 0; z < worldSizeZ; z++)
            {

                Vector3 pos = new Vector3(x * gridOffset, GenerateNoise(x, z, 8f, blockType == ObjType.Building) * noiseHeight, z * gridOffset);

                GameObject block = Instantiate(blockGameObject, pos, Quaternion.identity) as GameObject;

                block.transform.SetParent(this.transform);
                maxScale =  block.transform.localScale = new Vector3(block.transform.localScale.x, pos.y, block.transform.localScale.z);

                //StartCoroutine(BuildBlock(1f, 0.01f, block, Vector3.zero, maxScale)); // Transition from scale 0 to intended scale

                if (blockType == ObjType.Building)
                {


                    block.name = "Building: " + buildingCount;

                    offsetY = ground.objectHeightOffset;

                    float chanceForRandomHeight = Random.Range(1f, 10f);
                    if (chanceForRandomHeight < randomizeHeight)
                    {
                        block.transform.localScale = new Vector3(block.transform.localScale.x, Random.Range(noiseHeight / 2, noiseHeight * 1.2f), block.transform.localScale.z);
                    }

                    block.GetComponent<BuildingData>().index = 5;
                    block.GetComponent<Renderer>().material = buildings[5].material;

                    if (block.gameObject.transform.localScale.y <= 0.1)
                    {
                        block.gameObject.transform.localScale = Vector3.one;

                        block.GetComponent<BuildingData>().index = 6;
                        block.GetComponent<Renderer>().material = buildings[6].material;
                    }

                    if (buildingCount == (int)quarter1)
                    {
                        block.name = "Ward 1 Main Building";
                        AddSphereCollider(block, 1);
                    }
                    if (buildingCount == (int)quarter2)
                    {
                        block.name = "Ward 2 Main Building";
                        AddSphereCollider(block, 2);
                    }
                    if (buildingCount == (int)quarter3)
                    {
                        block.name = "Ward 3 Main Building";
                        AddSphereCollider(block, 3);
                    }
                    if (buildingCount == (int)quarter4)
                    {
                        block.name = "Ward 4 Main Building";
                        AddSphereCollider(block, 4);
                    }

                    buildingCount++;
         
                }
                else
                {

                    if (count == (((worldSizeX * worldSizeZ) / 2) + worldSizeX / 2))
                    {
                        block.name = "Centre";
                        centerGameObject = block.gameObject;
                    }
                    count++;
                }
                block.transform.position = new Vector3(block.transform.position.x + offsetX, (block.transform.localScale.y / 2) + offsetY, block.transform.position.z + offsetZ);
            }

        }
        if (blockType == ObjType.Ground)
        {
            GetComponent<Transform>().position = new Vector3((map.actualSize - this.actualSize) / 2, GetComponent<Transform>().position.z, (map.actualSize - this.actualSize) / 2);

            centerGameObject.AddComponent<SphereCollider>();
            centerGameObject.GetComponent<SphereCollider>().radius = map.worldSizeX / 3f;
            centerGameObject.GetComponent<SphereCollider>().isTrigger = true;

            if ((worldSizeX * worldSizeZ) % 2 == 0)
            {
                centerGameObject.GetComponent<SphereCollider>().center = new Vector3(-0.9f, 0, -1f);
            }

            centerGameObject.AddComponent<CentreCollider>();

        }
        if (blockType == ObjType.Building)
        {
            centerBuildingGameObject = map.gameObject.transform.GetChild((int)(worldSizeX*worldSizeZ)/2).gameObject;
            maxDistance = Vector2.Distance(centerBuildingGameObject.transform.position, map.gameObject.transform.GetChild(map.gameObject.transform.childCount - 1).transform.position) * 1.5f;
        }

        perlinNoiseObject.transform.localScale = new Vector3(worldSizeX / 3, 1, worldSizeX / 3);
        float v = perlinNoiseObject.transform.localScale.x * 3 + perlinNoiseObject.transform.localScale.x / 2;
        perlinNoiseObject.transform.position = new Vector3(v, 0, v);
    }

    private void AddSphereCollider(GameObject parent, int index)
    {
        parent.AddComponent<SphereCollider>();
        parent.GetComponent<SphereCollider>().radius = scale;
        parent.GetComponent<SphereCollider>().isTrigger = true;

        parent.AddComponent<BuildingCollider>();
        parent.GetComponent<BuildingData>().index = index;
        //parent.GetComponent<BuildingData>().distanceParameter = maxDistance - Vector2.Distance(centerGameObject.transform.position, parent.transform.position);
        parent.GetComponent<Renderer>().material = buildings[index].material;
        parent.tag = "MainBuilding";
    }

    private float GenerateNoise(int x, int z, float detailScale, bool building)
    {
        float xNoise = (x + this.transform.position.x) / detailScale;
        float zNoise = (z + this.transform.position.z) / detailScale;

        if(building)
        {
            return perlinNoiseObject.GetComponent<PerlinNoise>().perlin(x, z);
        }

        return perlinNoiseObject.GetComponent<PerlinNoise>().customPerlin(x, z);
        //return Mathf.PerlinNoise(xNoise, zNoise);
    }

    IEnumerator BuildBlock(float time, float waitTime, GameObject go, Vector3 minT, Vector3 maxT)
    {
        float i = 0f;
        float rate = (1f / time);
        while (i < 1f)
        {
            i += Time.deltaTime * rate;
            go.transform.localScale = Vector3.Lerp(minT, maxT, i);
            yield return new WaitForSeconds(waitTime);
        }
    }




}
