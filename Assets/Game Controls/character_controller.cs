﻿using UnityEngine;
        {
            animator.Play("Attack");
        }
            {
                print("Too far away!");
            }
            {
                Physics.Raycast(transform.position, Quaternion.LookRotation(target_selector.target.transform.position - transform.position).eulerAngles, out hit);
                if (hit.collider.gameObject != target_selector.target)
                {
                    print("Attack blocked by an object!");
                }
                {
                    target_selector.target.BroadcastMessage("Damage", damage);
                }
            }