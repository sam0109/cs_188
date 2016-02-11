using UnityEngine;using System.Collections;using UnityEngine.UI;public class character_controller : MonoBehaviour {    Animation animate;    public float range;    public float damage;    target_selector target_selector;    RaycastHit hit;    // Use this for initialization    void Start () {
        animate = gameObject.GetComponent<Animation>();
        target_selector = GameObject.FindGameObjectWithTag("Targeter").GetComponent<target_selector>();
    }		// Update is called once per frame	void Update () {	}    public void Attack ()    {        if (animate)
        {
            animate.Play("Attack");
        }        if (target_selector.target)        {            if ((target_selector.target.transform.position - transform.position).magnitude > range)
            {
                print("Too far away!");
            }            else
            {
                target_selector.target.BroadcastMessage("Damage", damage);
            }        }        else        {            print("Nothing selected!");        }    }}