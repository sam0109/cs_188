using UnityEngine;
using System.Collections;

public class takeDamage : MonoBehaviour {
    public ParticleSystem explode;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Damage(float damage)
    {
        Instantiate(explode, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
