using UnityEngine;
using System.Collections;

public class takeDamage : MonoBehaviour {
    public ParticleSystem explode;
    public Vector3 height;
    public GameObject healthBar;
    GameObject myHealthBar;
    public float health;
    public float currentHealth;
	public int armorClass;
	public string damageText;

	// Use this for initialization
	void Start () {
        currentHealth = health;
        myHealthBar = (GameObject)Instantiate(healthBar, height, Quaternion.identity);
        myHealthBar.transform.SetParent(gameObject.transform, false);
	}

    //void Damage(float damage)
	void Damage(attack_values values)
    {
        print("ouch");
		float chanceToHit = values.diceRollToHit;
		float attackDamage = values.attackDamageWithDice;
		ShowMessage ("Your potential attack damage is " + attackDamage, 3);

		int targetArmor = 10; //This needs to change
		if (chanceToHit > targetArmor)
		{
			ShowMessage ("You hit!", 2);

			currentHealth -= attackDamage;
			if (currentHealth <= 0) {
				Instantiate (explode, transform.position, Quaternion.identity);
				//GameControl.control.state.frame_markers
				Destroy (gameObject);
			}
		}
		else 
		{
			ShowMessage ("You couldn't make it through the armor. Your attack failed.", 3);
		}
    }

	IEnumerator ShowMessage(string message, float delay)
	{
		damageText = message;
		yield return new WaitForSeconds (delay);
		damageText = "";
	}
}
