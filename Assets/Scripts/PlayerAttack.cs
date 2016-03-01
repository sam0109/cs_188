using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {
    public void Attack ()
    {
		foreach (FrameMarkerController character in GameControl.control.frame_markers) 
		{
			character.Attack ();
		}
    }
}
