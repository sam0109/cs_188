using UnityEngine;
using System.Collections;

public class ValueChanged : MonoBehaviour {

	public target_selector targetSel;

	public void DropdownChanged(int value)
	{
		targetSel.target.transform.parent.BroadcastMessage ("SetModel", value); 
	}
}
