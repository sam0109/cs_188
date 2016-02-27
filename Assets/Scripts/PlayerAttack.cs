using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {
    public void Attack ()
    {
		GameObject[] characters = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject character in characters) 
		{
			character.GetComponent<character_controller> ().Attack ();
			string myPlayer = gameObject.transform.parent.GetComponent<FrameMarkerController> ().owner;
			string playerID = GameControl.control.match.SelfParticipantId;
			if (GameControl.control.isMyTurn && myPlayer == playerID) 
			{
				if (character.GetComponent<Animation> () ["Attack"]) 
				{
					character.GetComponent<Animation>().Play ("Attack");
				}
			}
		}
    }
}
