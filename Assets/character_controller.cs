using UnityEngine;using System.Collections;using UnityEngine.UI;public class character_controller : MonoBehaviour {    public Animator animator;    public target_selector target_selector;    public float damage;
    // Use this for initialization
    void Start () {	    	}		// Update is called once per frame	void Update () {	}    public void Attack ()    {        animator.Play("Attack");        if (target_selector.target)
        {
            target_selector.target.BroadcastMessage("Damage", damage);
        }        else
        {
            print("Nothing selected!");
        }    }}