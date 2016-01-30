﻿using UnityEngine;
using System.Collections;

public class target_selector : MonoBehaviour {
    public GameObject target;
    RaycastHit hit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            target = hit.collider.gameObject;
        }
	}
}
