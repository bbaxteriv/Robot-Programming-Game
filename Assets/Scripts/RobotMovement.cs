using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Handles the movement of a user-controlled robot
*/

public class RobotMovement : MonoBehaviour
{
    // (x,y) position of robot
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
    private TileMapGenerator mapGenerator;
    private bool selected;
    private Color startColor;

    /*
    Start is called before the first frame update
    */
    void Start()
    {
        float posX = xCoord * gridSize;
        float posY = yCoord * gridSize;
        this.transform.position = new Vector2(posX, posY);
        this.mapGenerator = this.manager.GetComponent<TileMapGenerator>();
        this.selected = false;
        this.startColor = GetComponent<Renderer>().material.color;
    }

    /*
    Update is called once per frame
    Current functionality: detects arrow key presses and handles movement
    with movement frames
    */
    void Update()
    {
        if (this.selected)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveUp();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveDown();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.selected = false;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }
        this.transform.position = new Vector2(xCoord * gridSize, yCoord * gridSize);
    }

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

    void OnMouseDown()
    {
        if (!this.selected)
        {
            this.selected = true;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 1);
        }
    }

    /*
    Teleport the robot to position (x,y), if possible
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
    Checks if a tile can be occupied by a robot
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
        return this.mapGenerator.map[x,y];
    }
}
