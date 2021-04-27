using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // (x,y) position of spawner
    public int xCoord = 0;
    public int yCoord = 0;
    // tile to Unity grid scaling constant
    public float gridSize = 1;
    // to access map generator script
    public GameObject manager;
    private TileMapGenerator mapGenerator;

    /*
    Start is called before the first frame update
    */
    void Start()
    {
        float posX = xCoord * gridSize;
        float posY = yCoord * gridSize;
        this.transform.position = new Vector2(posX, posY);
        this.mapGenerator = this.manager.GetComponent<TileMapGenerator>();
    }

    /*
    Update is called once per frame
    */
    void Update()
    {
        //this.transform.position = new Vector2(xCoord * gridSize, yCoord * gridSize);
    }

    /*
    Teleport the enemy to position (x,y), if possible
    */
    void Teleport(int x, int y)
    {
        if (CheckTile(x, y))
        {
            this.xCoord = x;
            this.yCoord = y;
            this.transform.position = new Vector2(xCoord * gridSize, yCoord * gridSize);
        }
    }

    /*
    Checks if a tile can be occupied by an spawner
    currently: tiles that can't be occupied are not in map bounds
    */
    bool CheckTile(int x, int y)
    {
        return (GetTile(x, y) >= 0);
    }

    /*
    Returns the tile index at the position (x,y)
    */
    int GetTile(int x, int y)
    {
        if (x < 0 || x >= this.mapGenerator.rows)
        {
            return -1;
        }
        if (y < 0 || y >= this.mapGenerator.columns)
        {
            return -1;
        }
        return this.mapGenerator.map[x, y];
    }
}
