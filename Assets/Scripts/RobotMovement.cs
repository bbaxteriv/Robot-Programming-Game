using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blocker;

/*
Handles the movement of a user-controlled robot
*/

public class RobotMovement : ObjectMovement, IResettable
{
    /*
    Start is called before the first frame update
    */
    public override void Start()
    {
        base.Start();
        this.selected = false;
    }

    /*
    Update is called once per frame
    Current functionality: detects arrow key presses and handles movement
    with movement frames
    */
    public override void Update()
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
            this.programController.Robot = null;
        }
        this.transform.position = new Vector2(xCoord * gridSize, yCoord * gridSize);
    }

    public override void OnMouseDown()
    {
        if (!this.selected)
        {
            if (this.programController.Robot == null)
            {
                this.selected = true;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 1);
                this.programController.Robot = gameObject;
            }
        }
    }

    [Command]
    void Move(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            MoveLeft();
        }
    }

    public void Reset()
    {
        Teleport(1, 1);
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
        if (this.mapGenerator.map[x,y] != 0)
        {
          Debug.Log("In conditional:" + this.mapGenerator.map[x,y]);
          return -1;
        }
        Debug.Log(this.mapGenerator.map[x,y]);
        return this.mapGenerator.map[x,y];
    }
}
