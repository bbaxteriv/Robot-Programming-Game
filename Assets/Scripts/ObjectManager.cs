using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Manages all the objects on the map
*/

public class ObjectManager : MonoBehaviour
{
    private TileMapGenerator mapGenerator;
    public GameObject robotPrefab;
    public List<GameObject> objectList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        this.mapGenerator = this.gameObject.GetComponent<TileMapGenerator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    Spawns a robot at (x,y)
    */
    public void SpawnRobot(int x, int y)
    {
        GameObject robot = Instantiate(this.robotPrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
        RobotMovement script = robot.GetComponent<RobotMovement>();
        script.xCoord = x;
        script.yCoord = y;
        script.manager = this.gameObject;
        this.objectList.Add(robot);
    }
}
