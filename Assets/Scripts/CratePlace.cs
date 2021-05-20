using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratePlace : MonoBehaviour
{
    public GameObject CratePrefab;
    private TileMapGenerator mapGenerator;

    [Range(0.0f,0.02f)]
    public float crateDensity;

    // Start is called before the first frame update
    void Start()
    {

      this.mapGenerator = this.gameObject.GetComponent<TileMapGenerator>();
      System.Random random = new System.Random(this.mapGenerator.seed.GetHashCode());
      int areaOfMap = this.mapGenerator.rows*this.mapGenerator.columns;
      Debug.Log(areaOfMap * crateDensity);
      int counter = 0;

      for (int i = 0; i < (areaOfMap * crateDensity); i++) {
        int crateX = random.Next(0,this.mapGenerator.rows);
        int crateY = random.Next(0,this.mapGenerator.columns);
        if (this.mapGenerator.map[crateX,crateY]==0) {
            Vector3 position = new Vector3(crateX, crateY-(float)0.4, 0);
            GameObject Crate = Instantiate(CratePrefab, position, Quaternion.identity);
          //Debug.Log("crating");
        } else {
        //  Debug.Log("check failed " + i);
            i--;
        }
        counter++;
        if (counter > areaOfMap) {
          break;
        }
      }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
