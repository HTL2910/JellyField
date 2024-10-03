using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private int height = 5;
    [SerializeField] private int width = 5;
    public GameObject[,] map;
    [SerializeField] private GameObject[] arrCubes;
    [SerializeField] private GameObject backgroundMap;
    [SerializeField] private int countSpawnPos=2;
    [SerializeField] private int countCube=2;
    [SerializeField] private List<GameObject> posListSpawn;

    void Start()
    {
        map = new GameObject[width, height];

        InitMap();
    
    }
    private void InitMap()
    {
        CreateSpawnPos();
        CreateTileMap();
        CreateCubes();
    }

    private void CreateTileMap()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject objectTmp = Instantiate(backgroundMap);
                objectTmp.name = "[" + i + "," + j + "]";
                objectTmp.transform.position = new Vector3(i, j, 0);
                objectTmp.transform.parent = this.transform;
                
            }
        }
    }

    private void CreateSpawnPos()
    {
        for (int i = 0; i < countSpawnPos; i++)
        {

            GameObject objectTmp = Instantiate(backgroundMap);
            objectTmp.name = "CountSpawn " + i;
            Vector3 screenwidthCenter = new Vector3(Screen.width / 2, 0, 0);
            Vector3 toWorld = Camera.main.ScreenToWorldPoint(screenwidthCenter);
            objectTmp.transform.position = new Vector3(toWorld.x + i - 0.5f+(i*0.2f), -2, 0);
            objectTmp.transform.parent = this.transform;

        
            posListSpawn.Add(objectTmp);

        }
        SpawnNewCube();
    }
    public void SpawnNewCube()
    {
        for(int i=0;i<posListSpawn.Count;i++)
        {
            if (posListSpawn[i].transform.childCount == 0)
            {
                int indexCubes = Random.Range(0, arrCubes.Length);
                GameObject cubeSpawnobj = Instantiate(arrCubes[indexCubes], this.transform);
                cubeSpawnobj.GetComponent<CubeInteract>().isMove = true;
                cubeSpawnobj.transform.position = posListSpawn[i].transform.position;
                cubeSpawnobj.transform.SetParent(posListSpawn[i].transform);
            }
           
        }
       
    }
    private void CreateCubes()
    {
       
        for(int i=0;i<countCube;i++)
        {
            int indexCubes = Random.Range(0, arrCubes.Length);
            GameObject tmpObject = Instantiate(arrCubes[indexCubes],this.transform);
            tmpObject.name = arrCubes[indexCubes].name + " index: "+ i.ToString();
            tmpObject.GetComponent<CubeInteract>().isMove = false;
            int attempts = 0;
            int posX, posY;
            do
            {
                posX = Random.Range(0, width);
                posY = Random.Range(0, height);
                attempts++;
            }
            while (map[posX, posY] != null );

            
            tmpObject.transform.position = new Vector3(posX, posY, 0);
            map[posX, posY] = tmpObject;
            

            
            
        }
        

    }
}
