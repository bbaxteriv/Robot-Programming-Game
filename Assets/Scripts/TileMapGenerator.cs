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

    [Range(0,100)]
    public int randomWaterPercent;

    [Range(0,100)]
    public int flowerAppearance;

    public int[,] map;
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
              //the 0 value in the map is assigned to the empty (or grass tile spot)
              if (map[i,j] == 1) {
                //in unity, the first one is a stone tile, this will change, but until we know how many tiles we need its hard to choose.
                tileValue = 32;
              } else if (map[i,j] == 2){
                string checker = ListCloseSimilarTiles(i,j,2);
                if (checker == ""){
                  tileValue = 33;
                } else if (checker == "L") {
                  tileValue = 34;
                } else if (checker == "R") {
                  tileValue = 35;
                } else if (checker == "B") {
                  tileValue = 36;
                } else if (checker == "T") {
                  tileValue = 37;
                } else if (checker == "LB") {
                  tileValue = 38;
                } else if (checker == "RB") {
                  tileValue = 39;
                } else if (checker == "RT") {
                  tileValue = 40;
                } else if (checker == "LT") {
                  tileValue = 41;
                } else if (checker == "LRB") {
                  tileValue = 42;
                } else if (checker == "RBT") {
                  tileValue = 43;
                } else if (checker == "LRT") {
                  tileValue = 44;
                } else if (checker == "LBT") {
                  tileValue = 45;
                } else if (checker == "LR") {
                  tileValue = 46;
                } else if (checker == "TB") {
                  tileValue = 47;
                } else if (checker == "LRTB") {
                  tileValue = 48;
                }
              } else {
              /*  int myRand = Random.Range(0,100);
                Debug.Log(myRand);
                if (myRand > flowerAppearance) {
                    tileValue = Random.Range(0, 15);
                    Debug.Log("gras");
                } else {
                    tileValue = Random.Range(16, 31);
                    Debug.Log("flower");
                }*/
                tileValue = (Random.Range(1,100) > flowerAppearance)? Random.Range(0, 15): Random.Range(16, 31);
                Debug.Log("Here");
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
        System.Random random = new System.Random(seed.GetHashCode());
      for (int x = 0; x < rows; x ++) {
          for (int y = 0; y < columns; y ++) {
            if (map[x,y] == 1 && CountCloseTiles(x,y, 1) < numTilesToKeep) {
              newMap[x,y] = 0;
            }
            else {
              if (map[x,y] == 0) {
                newMap[x,y] = (random.Next(0,100) < randomWaterPercent)? 2: 0;
              }
            }
          }
      }
      map = newMap;

    }

    int CountCloseTiles(int x, int y, int value) {
      //Debug.Log("checking it at " + x.ToString() + ", " + y.ToString());
      //Debug.Log("boundary at " + rows.ToString() + ", " + columns.ToString());
      //Debug.Log("map boundary at " + map.Length.ToString() + ", " + map.Length.ToString());

      int tileCount = 0;
      //annoying to do the couble conditionals, but otherwise its harder to check the value of an array
      //maybe use && to prevent the second from being run?
      if (x != 0) {
        if (map[x-1,y] == value) {
          tileCount++;
        }
      }
      if (x != rows-1) {
        if (map[x+1,y] == value) {
          tileCount++;
        }
      }
      if (y != 0) {
        if (map[x,y-1] == value) {
          tileCount++;
        }
      }
      if (y != columns-1) {
        if (map[x,y+1] == value) {
          tileCount++;
        }
      }
      return tileCount;
    }

    string ListCloseSimilarTiles(int x, int y, int value) {
      string toReturn = "";
      if (x != 0) {
        if (map[x-1,y] == value) {
          toReturn = toReturn + "L";
        }
      }
      if (x != rows-1) {
        if (map[x+1,y] == value) {
          toReturn = toReturn +"R";
        }
      }
      if (y != 0) {
        if (map[x,y-1] == value) {
          toReturn = toReturn + "B";
        }
      }
      if (y != columns-1) {
        if (map[x,y+1] == value) {
          toReturn= toReturn + "T";
        }
      }
      return toReturn;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
