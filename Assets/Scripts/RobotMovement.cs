using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Blocker;

/*
Handles the movement of a user-controlled robot
*/

public class RobotMovement : ObjectMovement, IResettable
{
    private RobotHealth robotHealth;
    public Text textEdit;

    public string SequenceString = "Right, Left, Right, Left, Right,";
    /*
    Start is called before the first frame update
    */
    public override void Start()
    {
        base.Start();
        this.robotHealth = this.gameObject.GetComponent<RobotHealth>();
        this.selected = false;
        // textEdit = GameObject.Find("/Canvas/ResourceText").GetComponent<Text>();
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
            this.blockManager.robot = null;
            this.blockManager.UpdateCommandObject();
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
                this.blockManager.robot = this;
                this.blockManager.UpdateCommandObject();
            }
        }
    }

    [Command]
    void Move_Left(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            MoveLeft();
        }
    }

    [Command]
    void Move_Down(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            MoveDown();
        }
    }

    [Command]
    void Move_Right(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            MoveRight();
        }
    }

    [Command]
    void Move_Up(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            MoveUp();
        }
    }

    public void Reset()
    {
        Teleport(1, 1);
    }

    /*
    Checks if a tile can be occupied by a robot
    currently: tiles that can't be occupied are not in map bounds
    */
    public override bool CheckTile(int x, int y)
    {
        return (GetTile(x, y) >= 0);
    }

    /*
    Returns the tile index at the position (x,y)
    */
    public override int GetTile(int x, int y)
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
            // Debug.Log("In conditional:" + this.mapGenerator.map[x,y]);
            return -1;
        }
        // Debug.Log(this.mapGenerator.map[x,y]);
        return this.mapGenerator.map[x,y];
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
      //Debug.Log(other);
      if (other.ToString() == "CratePrefab(Clone) (UnityEngine.BoxCollider2D)")
      {
        //Debug.Log("its a crate!");
        Destroy(other.gameObject);


        string currentResource = textEdit.text.Split(' ')[2];
        textEdit.text = "Scrap Metal: " + (int.Parse(currentResource) + 3);


        }// else if (other.ToString() == "Enemy (UnityEngine.BoxCollider2D)") {

        //robotHealth.Heal(5);


    else if (other.ToString() == "Enemy (UnityEngine.BoxCollider2D)") {

        robotHealth.TakeDamage(10);
      }





    }
}
