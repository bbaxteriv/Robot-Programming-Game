using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject container;

    public int rows;
    public int columns;

    public string seed;
    public bool useRandomSeed;

    public int numTilesToKeep;

    [Range(0,100)]
    public int randomFillPercent;

    int[,] map;
    List<int> tilePrefabIndex = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        //debug message
        Debug.Log("now mapping a " + rows.ToString() + " by " + columns.ToString() + "map with a fill of " + randomFillPercent.ToString());
        Debug.Log("mapping with " + tilePrefabs.Length.ToString() + " types of tiles");
        GenerateMap();
        // loop through the spaces
        for(int i = 0; i<rows; i++)
        {
            for(int j = 0; j<columns; j++)
            {
                int tileValue;
                if (map[i,j] != 0) {
                  tileValue = Random.Range(1, tilePrefabs.Length);
                } else {
                  tileValue = 0;
                }
                //set a random prefab

                tilePrefabIndex.Add(tileValue);

                Vector3 position = new Vector3(i, j, 0);

                GameObject tile = Instantiate(tilePrefabs[tileValue], position, Quaternion.identity);

                tile.transform.parent = container.transform;
            }
        }
    }

    //generates map, maybe combine with fandom fill?
    void GenerateMap() {
        map = new int[rows,columns];
        RandomFillMap();
        SmoothMap();
    }

    //dubgging function, prints the current map to the console
    void CurrentMap() {
      if (!(map is null)) {
        string Map = "";
        for (int x = 0; x < rows; x ++) {
            for (int y = 0; y < columns; y ++) {
              Map = Map + map[x,y] + "\t";
            }
            Map = Map + "\n";
        }
        Debug.Log("the map is currently\n" + Map);
      } else {
      Debug.Log("the map is currently null");
      }
    }


    //generates the actual map
    //based on code from this tutorial https://learn.unity.com/tutorial/generating-content?projectId=5c514ac8edbc2a0020694815#5c7f8528edbc2a002053b6d7
    //i have modified it quite a bit.
    void RandomFillMap() {

      //uses the a seed or not
        if (useRandomSeed) {
            seed = Time.time.ToString();
        }

        System.Random random = new System.Random(seed.GetHashCode());

      //loops through the array
        for (int x = 0; x < rows; x ++) {
            for (int y = 0; y < columns; y ++) {
                  map[x,y] = (random.Next(0,100) < randomFillPercent)? 1: 0;
                  //Debug.Log("map[" + x.ToString() + ", " + y.ToString() + "] is " + map[x,y].ToString());
                }
          }
      }

    void SmoothMap() {
      int[,] newMap = map;
      for (int x = 0; x < rows; x ++) {
          for (int y = 0; y < columns; y ++) {
            if (map[x,y] == 1 && CountCloseTiles(x,y) < numTilesToKeep) {
              newMap[x,y] = 0;
            }
          }
      }
      map = newMap;

    }

    int CountCloseTiles(int x, int y) {
      Debug.Log("checking it at " + x.ToString() + ", " + y.ToString());
      Debug.Log("boundary at " + rows.ToString() + ", " + columns.ToString());
      Debug.Log("map boundary at " + map.Length.ToString() + ", " + map.Length.ToString());

      int tileCount = 0;
      //annoying to do the couble conditionals, but otherwise its harder to check the value of an array
      //maybe use && to prevent the second from being run?
      if (x != 0) {
        if (map[x-1,y] == 1) {
          tileCount++;
        }
      }
      if (x != rows-1) {
        if (map[x+1,y] == 1) {
          tileCount++;
        }
      }
      if (y != 0) {
        if (map[x,y-1] == 1) {
          tileCount++;
        }
      }
      if (y != columns-1) {
        if (map[x,y+1] == 1) {
          tileCount++;
        }
      }
      return tileCount;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
