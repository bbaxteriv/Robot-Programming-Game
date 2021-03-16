using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject container;

    public int rows;
    public int columns;

    List<int> tilePrefabIndex = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        // loop through the spaces
        for(int i = 0; i<rows; i++)
        {
            for(int j = 0; j<rows; j++)
            {
                //set a random prefab

                int RandomTileNumber = Random.Range(0, tilePrefabs.Length);

                tilePrefabIndex.Add(RandomTileNumber);

                Vector3 position = new Vector3(i, j, 0);

                GameObject tile = Instantiate(tilePrefabs[RandomTileNumber], position, Quaternion.identity);

                tile.transform.parent = container.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
