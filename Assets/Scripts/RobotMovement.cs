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

    }
}
