using UnityEngine;
using System.Collections;

public class ValueChanged : MonoBehaviour {

	public target_selector targetSel;
    public GameControl gameController;

	public void DropdownChanged(int value)
	{
        gameController.frame_markers[targetSel.target.GetComponentInParent<FrameMarkerController>().frame_marker_identifier] = value;
	}
}
