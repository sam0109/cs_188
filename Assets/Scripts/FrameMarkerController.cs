﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class FrameMarkerController : MonoBehaviour {
    public int frame_marker_identifier;
    target_selector targeter;
    GameObject particles;
    bool particlesPlaying;
    int current_model_num;
    GameObject current_model;
    Vuforia.MarkerBehaviour marker;
    GameObject myHealthBar;
    static ItemDataBaseList inventoryItemList;

    public void Start()
    {
        inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");

        particlesPlaying = false;
        current_model_num = -1;
        targeter = GameObject.FindGameObjectWithTag("Targeter").GetComponent<target_selector>();
        marker = gameObject.GetComponent<Vuforia.MarkerBehaviour>();
        frame_marker_identifier = marker.Marker.MarkerID;
    }

    public void SetModel(int i)
    {
        if (i >= 0 && i < GameControl.control.models.Count)
        {
            if (current_model)
            {
                Destroy(current_model);
            }
            current_model = Instantiate(GameControl.control.models[i]);
            current_model.transform.SetParent(gameObject.transform, false);
            if (GameControl.control.getActor(frame_marker_identifier).maxHealth > 0)
            {
                myHealthBar = (GameObject)Instantiate(GameControl.control.healthbar, new Vector3(0, 1.5f, 0), Quaternion.identity);
                myHealthBar.transform.SetParent(gameObject.transform, false);
            }
            else if(myHealthBar)
            {
                Destroy(myHealthBar);
            }
        }
        else
        {
            Debug.Log(i + " is out of range");
        }
    }

    public void Update()
    {
        if (marker.CurrentStatus == Vuforia.TrackableBehaviour.Status.TRACKED)
        {
            if (current_model_num != GameControl.control.getActor(frame_marker_identifier).model)
            {
                current_model_num = GameControl.control.getActor(frame_marker_identifier).model;
                SetModel(current_model_num);
            }

            if (targeter.target == current_model)
            {
                if (!particlesPlaying)
                {
                    particlesPlaying = true;
                    particles = Instantiate(GameControl.control.highlighted);
                    particles.transform.SetParent(gameObject.transform, false);
                }
            }
            else if (particlesPlaying)
            {
                particlesPlaying = false;
                Destroy(particles);
            }
        }
        else
        {
            if (current_model)
            {
                Destroy(current_model);
                current_model_num = -1;
            }
        }
    }

    public void Action(GameObject inventory)
    {
        //string playerID = GameControl.control.match.SelfParticipantId;

        if (GameControl.control.isMyTurn && 
            GameControl.control.getActor(frame_marker_identifier).player == GameControl.control.playerID &&
            GameControl.control.getActor(frame_marker_identifier).isPlayer == true)
        {

            if (targeter.target)
            {
                if (GameControl.control.rev_model_lookup[GameControl.control.getActor(targeter.target.GetComponentInParent<FrameMarkerController>().frame_marker_identifier).model] == "Treasure Chest")
                {
                    string item = GameControl.control.getActor(targeter.target.GetComponentInParent<FrameMarkerController>().frame_marker_identifier).chestItem;

                    GameControl.control.getActor(frame_marker_identifier).inventory.Add(item);

                    Inventory mainInventory = inventory.GetComponent<Inventory>();

                    int id = 0;

                    for (int i = 0; i < inventoryItemList.itemList.Count; i++)
                    {
                        if (inventoryItemList.itemList[i].itemName == item)
                        {
                            id = inventoryItemList.itemList[i].itemID;
                        }
                    }

                    mainInventory.addItemToInventory(id);

                    GameControl.control.updateMarker(targeter.target.GetComponentInParent<FrameMarkerController>().frame_marker_identifier, "Sphere");
                }
                else {

                    int diceRollHitOrNot = randomNum(20);

                    int strength = GameControl.control.myCharacter.str;

                    stat_converter mod = new stat_converter(strength);
                    int strengthMod = mod.modifierValue;

                    int diceRollDamage = randomNum(8);
                    int attackDamage = strengthMod + diceRollDamage;

                    attack_values values = new attack_values(diceRollHitOrNot, attackDamage);

                    //if ((targeter.target.transform.position - transform.position).magnitude > GameControl.control.getActor(frame_marker_identifier).range)
                    //{
                    //    print("Too far away!");
                    //}
                    //else
                    //{
                        targeter.target.transform.parent.BroadcastMessage("Damage", values);
                    //}
                }
            }
            else
            {
                print("Nothing selected!");
            }
        }
    }

    void Damage(attack_values values)
    {
        if (GameControl.control.getActor(frame_marker_identifier).maxHealth > 0)
        {
            int chanceToHit = values.diceRollToHit;
            int attackDamage = values.attackDamageWithDice;
            //ShowMessage("Your potential attack damage is " + attackDamage, 3);

            int targetArmor = 10; //This needs to change
            if (chanceToHit > targetArmor)
            {
                //ShowMessage("You hit!", 2);

                GameControl.control.getActor(frame_marker_identifier).currentHealth -= attackDamage;
                if (GameControl.control.getActor(frame_marker_identifier).currentHealth <= 0)
                {
                    Instantiate(GameControl.control.explode, transform.position, Quaternion.identity);
                    GameControl.control.updateMarker(frame_marker_identifier, "Sphere");
                }
            }
        }
        /*else
        {
            ShowMessage("You couldn't make it through the armor. Your attack failed.", 3);
        }*/
    }

    /*IEnumerator ShowMessage(string message, float delay)
    {
        damageText = message;
        yield return new WaitForSeconds(delay);
        damageText = "";
    }*/

    public int randomNum(int max)
    {
        int answ = Random.Range(1, max);
        return answ;
    }
}

public class attack_values
{
    public attack_values(int diceRollVal, int attackDamageVal)
    {
        diceRollToHit = diceRollVal;
        attackDamageWithDice = attackDamageVal;
    }

    public int diceRollToHit;
    public int attackDamageWithDice;
}

public class stat_converter
{
    public stat_converter(int stat)
    {
        modifierValue = (stat / 2) - 5;
    }

    public int modifierValue;
}
