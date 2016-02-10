using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {
    public void Attack ()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<character_controller>().Attack();
    }
}
