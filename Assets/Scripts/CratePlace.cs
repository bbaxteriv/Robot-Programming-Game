using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratePlace : MonoBehaviour
{
    public GameObject CratePrefab;
    private TileMapGenerator mapGenerator;

    public int numberOfCrates;

    // Start is called before the first frame update
    void Start()
    {

      this.mapGenerator = this.gameObject.GetComponent<TileMapGenerator>();
      Debug.Log("starting craplate");
      Debug.Log(this.mapGenerator.map);
      System.Random random = new System.Random(this.mapGenerator.seed.GetHashCode());
      for (int i = 0; i < numberOfCrates; i++) {
        int crateX = random.Next(0,this.mapGenerator.rows);
        int crateY = random.Next(0,this.mapGenerator.columns);
        Debug.Log(this.mapGenerator.map[crateX,crateY]);
        Debug.Log(this.mapGenerator.map[crateX,crateY]==0);
        if (this.mapGenerator.map[crateX,crateY]==0) {
          Vector3 position = new Vector3(crateX, crateY-(float)0.4, 0);
          GameObject Crate = Instantiate(CratePrefab, position, Quaternion.identity);
          //Debug.Log("crating");
      } else {
        //  Debug.Log("check failed " + i);
        i--;
      }
      }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
