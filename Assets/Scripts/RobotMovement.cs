using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Handles the movement of a user-controlled robot
*/

public class RobotMovement : ObjectMovement
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
}
