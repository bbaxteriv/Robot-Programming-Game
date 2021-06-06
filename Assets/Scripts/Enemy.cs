using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
/*
Handles the movement of an enemy
*/

public class Enemy : MonoBehaviour
{
    // (x,y) position of enemy
    public int xCoord = 0;
    public int yCoord = 3;
    // tile to Unity grid scaling constant
    public float gridSize = 1;
    // to access map generator script
    public GameObject manager;
    private TileMapGenerator mapGenerator;
    private EnemyHealth enemyHealth;

    public Text damageText;

    /*
    Start is called before the first frame update
    */
    void Start()
    {
        float posX = xCoord * gridSize;
        float posY = yCoord * gridSize;
        this.transform.position = new Vector2(posX, posY);
        this.mapGenerator = this.manager.GetComponent<TileMapGenerator>();
        this.enemyHealth = this.gameObject.GetComponent<EnemyHealth>();
        damageText = GameObject.Find("/ProgramWindow/DamageText").GetComponent<Text>();
        StartCoroutine(EnemyCoroutine());
    }

    IEnumerator EnemyCoroutine()
    {
        while (true)
        {
            int randomDirection = UnityEngine.Random.Range(1, 5);
            if (randomDirection == 1)
            {
                MoveRight();
            }
            else if (randomDirection == 2)
            {
                MoveLeft();
            }
            else if (randomDirection == 3)
            {
                MoveDown();
            }
            else if (randomDirection == 4)
            {
                MoveUp();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    /*
    Update is called once per frame
    */
    void Update()
    {
        //this.transform.position = new Vector2(xCoord * gridSize, yCoord * gridSize);
    }

    public void MoveUp()
    {
        if (CheckTile(this.xCoord, this.yCoord+1)){
            yCoord++;
            this.transform.position = new Vector2(xCoord * gridSize, yCoord * gridSize);
        }
    }

    public void MoveDown()
    {
        if (CheckTile(this.xCoord, this.yCoord-1)){
            yCoord--;
            this.transform.position = new Vector2(xCoord * gridSize, yCoord * gridSize);
        }
    }

    public void MoveRight()
    {
        if (CheckTile(this.xCoord+1, this.yCoord)){
            xCoord++;
            this.transform.position = new Vector2(xCoord * gridSize, yCoord * gridSize);
        }
    }

    public void MoveLeft()
    {
        if (CheckTile(this.xCoord-1, this.yCoord)){
            xCoord--;
            this.transform.position = new Vector2(xCoord * gridSize, yCoord * gridSize);
        }
    }

    /*
    Teleport the enemy to position (x,y), if possible
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
    Checks if a tile can be occupied by an enemy
    currently: tiles that can't be occupied are not in map bounds
    */
    bool CheckTile(int x, int y)
    {
        return (GetTile(x, y) >= 0);
    }

    /*
    Returns whether the tile is passable at the position (x,y)
    */
    int GetTile(int x, int y)
    {
        //Debug.Log(this.mapGenerator.map[0,0]);
        //checks if the tile's x is on the map
        if (x < 0 || x >= this.mapGenerator.rows)
        {
            return -1;
        }
        //checks if the tile's y is on the map
        if (y < 0 || y >= this.mapGenerator.columns)
        {
            return -1;
        }
        //checks if the tile map generator is anything but a grass tile
        //Debug.Log(x);
        //Debug.Log(y);
      //  Debug.Log(this.mapGenerator.map[x,y]);
        //Debug.Log(this.mapGenerator.map[x,y] != 0);
        if (this.mapGenerator.map[x,y] != 0)
        {
            return -1;
        }
        return this.mapGenerator.map[x,y];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

      //Debug.Log(other);
      if (other.ToString() == "Robot(Clone) (UnityEngine.BoxCollider2D)" || other.ToString() == "Robot (UnityEngine.BoxCollider2D)")
      {
            //  Debug.Log("its a robot! taking damage");
            damageText = GameObject.Find("/ProgramWindow/DamageText").GetComponent<Text>();
            enemyHealth.TakeDamage(Int32.Parse(damageText.text.Split(' ')[2]));
        //code needs to go here to increase scrap metal
      }


      // this is where the code to add scrap metal goes.


    }
}
