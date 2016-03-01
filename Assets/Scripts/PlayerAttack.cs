﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {
    public void Attack ()
    {
        print("arg");
        GameObject[] characters = GameObject.FindGameObjectsWithTag ("PlayerCharacter");
		foreach (GameObject character in characters) 
		{
			character.GetComponent<character_controller> ().Attack ();
			string myPlayer = gameObject.transform.parent.GetComponent<FrameMarkerController> ().owner;
			string playerID = GameControl.control.match.SelfParticipantId;
		}
    }
}
