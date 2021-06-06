using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
/*
Manages all the objects on the map
*/

public class ObjectManager : MonoBehaviour
{
    private TileMapGenerator mapGenerator;
    public GameObject robotPrefab;
    public GameObject enemyPrefab;
    public GameObject healthBarCanvasPrefab;
    public GameObject canvas;
    public GameObject blockPanel;
    public List<GameObject> objectList = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        
        this.mapGenerator = this.gameObject.GetComponent<TileMapGenerator>();
        StartCoroutine(MyCoroutine());
        //SpawnEnemy(1, 1);
    }

    IEnumerator MyCoroutine()
    {
        while (true)
        { 
            yield return new WaitForSeconds(10f);
            int xcoord = UnityEngine.Random.Range(6,39);
            int ycoord = UnityEngine.Random.Range(6,39);
            SpawnEnemy(xcoord,ycoord);
        }
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
        RobotMovement moveScript = robot.GetComponent<RobotMovement>();
        RobotHealth healthScript = robot.GetComponent<RobotHealth>();
        //robot.sortinglayer = robots;
        // GameObject healthBarCanvas = Instantiate(this.healthBarCanvasPrefab, new Vector3(x, y, 0), Quaternion.identity);
        // healthScript.healthBarCanvas = healthBarCanvas;
        moveScript.xCoord = x;
        moveScript.yCoord = y;
        moveScript.manager = this.gameObject;
        moveScript.canvas = this.canvas;
        moveScript.blockPanel = this.blockPanel;
        this.objectList.Add(robot);
    }
    public void DestroyRobot()
    {
        for (int i = 0; i < this.objectList.Count; i++)
        {
            if (this.objectList[i] != null && this.objectList[i].GetComponent<RobotMovement>().selected == true) {
                Destroy(this.objectList[i]);
                break;
            }
        }
    }
    public void SpawnEnemy(int x, int y)
    {
        GameObject enemy = Instantiate(this.enemyPrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
        Enemy moveScript = enemy.GetComponent<Enemy>();
        moveScript.xCoord = x;
        moveScript.yCoord = y;
        moveScript.manager = this.gameObject;
        //EnemyHealth healthScript = enemy.GetComponent<EnemyHealth>();

    }
}
