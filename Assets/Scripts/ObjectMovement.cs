using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Handles the movement of a generic object
*/

abstract public class ObjectMovement : MonoBehaviour
{
    // (x,y) position of object
    public int xCoord;
    public int yCoord;
    // tile to Unity grid scaling constant
    public float gridSize = 1;
    // movement frames
    public Sprite upFrame;
    public Sprite downFrame;
    public Sprite rightFrame;
    public Sprite leftFrame;
    // to access map generator script
    public GameObject manager;
    protected TileMapGenerator mapGenerator;
    // if the object is selected
    public bool selected;
    // to access the UI controller script
    public GameObject canvas;
    protected UIController programController;
    // to access the crate placement script
    public CratePlace cratePlace;

    /*
    Start is called before the first frame update
    */
    public virtual void Start()
    {
        float posX = xCoord * gridSize;
        float posY = yCoord * gridSize;
        this.transform.position = new Vector2(posX, posY);
        this.mapGenerator = this.manager.GetComponent<TileMapGenerator>();
        this.programController = this.canvas.GetComponent<UIController>();
        this.cratePlace =this.manager.GetComponent<CratePlace>();
    }

    /*
    Update is called once per frame
    Current functionality: detects arrow key presses and handles movement
    with movement frames
    */
    public abstract void Update();

    /*
    Moves the object up one tile, if possible
    */
    public void MoveUp()
    {
        this.GetComponent<SpriteRenderer>().sprite = upFrame;
        if (CheckTile(this.xCoord, this.yCoord+1))
        {
            yCoord++;
            this.transform.position = new Vector2(xCoord * gridSize, yCoord * gridSize);
        }
    }

    public void MoveDown()
    {
        this.GetComponent<SpriteRenderer>().sprite = downFrame;
        if (CheckTile(this.xCoord, this.yCoord-1))
        {
            yCoord--;
            this.transform.position = new Vector2(xCoord * gridSize, yCoord * gridSize);
        }
    }

    public void MoveRight()
    {
        this.GetComponent<SpriteRenderer>().sprite = rightFrame;
        if (CheckTile(this.xCoord+1, this.yCoord))
        {
            xCoord++;
            this.transform.position = new Vector2(xCoord * gridSize, yCoord * gridSize);
        }
    }

    public void MoveLeft()
    {
        this.GetComponent<SpriteRenderer>().sprite = leftFrame;
        if (CheckTile(this.xCoord-1, this.yCoord))
        {
            xCoord--;
            this.transform.position = new Vector2(xCoord * gridSize, yCoord * gridSize);
        }
    }

    public abstract void OnMouseDown();

    /*
    Teleport the robot to position (x,y), if possible
    */
    public void Teleport(int x, int y)
    {
        if (CheckTile(x, y))
        {
            this.xCoord = x;
            this.yCoord = y;
            this.transform.position = new Vector2(xCoord * gridSize, yCoord * gridSize);
        }
    }

    /*
    Checks if a tile can be occupied by a robot
    currently: tiles that can't be occupied are not in map bounds
    */
    public bool CheckTile(int x, int y)
    {
        return (GetTile(x, y) >= 0);
    }

    /*
    Returns the tile index at the position (x,y)
    */
    public int GetTile(int x, int y)
    {
        Debug.Log(this.mapGenerator.map[x,y]);
        if (x < 0 || x >= this.mapGenerator.rows)
        {
            return -1;
        }
        if (y < 0 || y >= this.mapGenerator.columns)
        {
            return -1;
        }
        if (this.mapGenerator.map[x,y] != 0)
        {
          return -1;
        }
        return this.mapGenerator.map[x,y];
    }

    public void CrateCheck() {
      if (this.cratePlace.crateMap[this.xCoord, this.yCoord]) {
        this.cratePlace.crateMap[this.xCoord, this.yCoord] = false;  
      }
    }



}
