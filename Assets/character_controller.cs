﻿using UnityEngine;
    // Use this for initialization
    void Start () {
        {
            target_selector.target.BroadcastMessage("Damage", damage);
        }
        {
            print("Nothing selected!");
        }