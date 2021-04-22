using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : ObjectHealth
{
    // Update is called once per frame
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TakeDamage(10);
        }
        healthBarCanvas.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + ((float) 0.7));
    }
}
