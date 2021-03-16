using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Handles the movement of a user-controlled robot
*/

public class RobotMovement : MonoBehaviour
{

    public int xCoord = 2;
    public int yCoord = -1;
    public float gridSize = 1;
    public Sprite upFrame;
    public Sprite downFrame;
    public Sprite rightFrame;
    public Sprite leftFrame;

    // Start is called before the first frame update
    void Start()
    {
        float posX = xCoord * gridSize;
        float posY = yCoord * gridSize;
        this.transform.position = new Vector2(posX, posY);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            yCoord++;
            this.GetComponent<SpriteRenderer>().sprite = upFrame;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            yCoord--;
            this.GetComponent<SpriteRenderer>().sprite = downFrame;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            xCoord++;
            this.GetComponent<SpriteRenderer>().sprite = rightFrame;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            xCoord--;
            this.GetComponent<SpriteRenderer>().sprite = leftFrame;
        }
        this.transform.position = new Vector2(xCoord * gridSize, yCoord * gridSize);
    }
}
