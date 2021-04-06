using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Manages the health of an object
*/
abstract public class ObjectHealth : MonoBehaviour
{
    // health attributes
    public int maxHealth;
    public int currentHealth;
    // for the health bar
    public GameObject healthBarCanvasPrefab;
    public GameObject healthBarCanvas;
    public HealthBar healthBar;

    // Start is called before the first frame update
    public void Start()
    {
        this.healthBarCanvas = Instantiate(this.healthBarCanvasPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        healthBar = healthBarCanvas.transform.GetChild(0).GetComponent<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    public abstract void Update();

    public void TakeDamage(int damage)
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
