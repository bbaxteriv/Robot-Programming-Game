using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHealth : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public GameObject healthBarCanvas;
    private HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = healthBarCanvas.transform.GetChild(0).GetComponent<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
        healthBarCanvas.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + ((float) 0.7));
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
            Destroy(healthBarCanvas);
        }
    }
}
