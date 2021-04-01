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
    public GameObject canvas;
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
        script.canvas = this.canvas;
        this.objectList.Add(robot);
    }
    public void DestroyRobot()
    {
        for (int i = 0; i < this.objectList.Count; i++)
        {
            //if (this.objectList[i].RobotMovement.selected == true) {
                Destroy(this.objectList[i]);
            //}
        }
    }
}
