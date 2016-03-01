using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {
    public void Attack (GameObject inventory = null)
    {
		foreach (FrameMarkerController character in GameControl.control.frame_markers) 
		{
			character.Action (inventory);
		}
    }
}
