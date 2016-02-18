using UnityEngine;
using System.Collections;

public class takeDamage : MonoBehaviour {
    public ParticleSystem explode;
    public Vector3 height;
    public GameObject healthBar;
    GameObject myHealthBar;
    public float health;
    public float currentHealth;
	// Use this for initialization
	void Start () {
        currentHealth = health;
        myHealthBar = (GameObject)Instantiate(healthBar, height, Quaternion.identity);
        myHealthBar.transform.SetParent(gameObject.transform, false);
	}

    void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
