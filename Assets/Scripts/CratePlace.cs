using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratePlace : MonoBehaviour
{
    public GameObject CratePrefab;
    private TileMapGenerator mapGenerator;

    public int numberOfCrates;
    public int crateValue;
    public bool[,] crateMap;
    public int crateXCoord;
    public int crateYCoord;
    public List<(int, int)> crateCoordsList = new List<(int, int)>();
    // Start is called before the first frame update
    void Start()
    {

      this.mapGenerator = this.gameObject.GetComponent<TileMapGenerator>();
      crateMap = new bool[this.mapGenerator.rows, this.mapGenerator.columns];
      for (int i = 0; i < this.mapGenerator.rows; i++) {
        for (int j = 0; j < this.mapGenerator.columns; j++) {
          crateMap[i,j] = false;
        }
      }

      Debug.Log("starting craplate");
      Debug.Log(this.mapGenerator.map);
      System.Random random = new System.Random(this.mapGenerator.seed.GetHashCode());
      for (int i = 0; i < numberOfCrates; i++) {
        int crateXCoord = random.Next(0,this.mapGenerator.rows);
        int crateYCoord = random.Next(0,this.mapGenerator.columns);
        Debug.Log(this.mapGenerator.map[crateXCoord,crateYCoord]);
        Debug.Log(this.mapGenerator.map[crateXCoord,crateYCoord]==0);
        if (this.mapGenerator.map[crateXCoord,crateYCoord]==0) {
          Vector3 position = new Vector3(crateXCoord, crateYCoord-(float)0.4, 0);
          GameObject Crate = Instantiate(CratePrefab, position, Quaternion.identity);
          Debug.Log("crating");
          crateMap[crateXCoord, crateYCoord] = true;
          crateCoordsList.Add((crateXCoord, crateYCoord));
        } else {
          Debug.Log("check failed " + i);
          i--;
        }
      }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
