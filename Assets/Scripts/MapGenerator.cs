using UnityEngine;
using System.Collections;
using System;

public class MapGenerator : MonoBehaviour {

    public int width;
    public int height;

    public string seed;
    public bool useRandomSeed;

    [Range(0,100)]
    public int randomFillPercent;

    int[,] map;

    void Start() {
      Debug.Log("now mapping a " + width.ToString() + " by " + height.ToString() + "map with a fill of " + randomFillPercent.ToString());
    //  CurrentMap();
      GenerateMap();
      CurrentMap();
      OnDrawGizmos();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            GenerateMap();
        }
    }

    void GenerateMap() {
        map = new int[width,height];
        RandomFillMap();

    }

    void CurrentMap() {
      if (!(map is null)) {
        string Map = "";
        for (int x = 0; x < width; x ++) {
            for (int y = 0; y < height; y ++) {
              Map = Map + map[x,y] + "\t";
            }
            Map = Map + "\n";
        }
        Debug.Log("the map is currently\n" + Map);
      } else {
      Debug.Log("the map is currently null");
      }
    }

    void RandomFillMap() {
        if (useRandomSeed) {
            seed = Time.time.ToString();
        }

        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x ++) {
            for (int y = 0; y < height; y ++) {
                if (x == 0 || x == width-1 || y == 0 || y == height -1) {
                    map[x,y] = 1;
                    //Debug.Log("map[" + x.ToString() + ", " + y.ToString() + "] is " + map[x,y].ToString());
                }
                else {
                    map[x,y] = (pseudoRandom.Next(0,100) < randomFillPercent)? 1: 0;
                    //Debug.Log("map[" + x.ToString() + ", " + y.ToString() + "] is " + map[x,y].ToString());
                }
            }
        }
    }


    void OnDrawGizmos() {
        if (!(map is null)) {
            for (int x = 0; x < width; x ++) {
                for (int y = 0; y < height; y ++) {
                    Gizmos.color = (map[x,y] == 1)?Color.black:Color.white;
                    Vector3 pos = new Vector3(-width/2 + x + .5f,0, -height/2 + y+.5f);

                    Gizmos.DrawCube(pos,Vector3.one);
                }
            }
            //Debug.Log("triggered, and drawn");
        }
        //Debug.Log("past conditional");
    }

}
