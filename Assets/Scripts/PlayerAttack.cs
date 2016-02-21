using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {
    public void Attack ()
    {
		GameObject[] characters = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject character in characters) {
			character.GetComponent<character_controller> ().Attack ();
		}
    }
}
