﻿using UnityEngine;using System.Collections;using UnityEngine.UI;public class character_controller : MonoBehaviour {    public Animation animate;    public target_selector target_selector;    public float range;    public float damage;    RaycastHit hit;    // Use this for initialization    void Start () {
        target_selector = GameObject.FindGameObjectWithTag("Targeter").GetComponent<target_selector>();
    }		// Update is called once per frame	void Update () {	}    public void Attack ()    {        //if (animate)
        //{
        //    animate.Play("Attack");
        //}        if (target_selector.target)        {            /*if ((target_selector.target.transform.position - transform.position).magnitude > range)
            {
                print("Too far away!");
            }            else
            {*/
                //Physics.Raycast(transform.position, Quaternion.LookRotation(target_selector.target.transform.position - transform.position).eulerAngles, out hit);
                //if (hit.collider.gameObject != target_selector.target)
                //{
                //    print("Attack blocked by an object!");
                //}                //else
                //{
                    target_selector.target.BroadcastMessage("Damage", damage);
                //}
            //}        }        else        {            print("Nothing selected!");        }    }}