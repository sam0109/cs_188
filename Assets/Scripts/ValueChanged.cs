﻿using UnityEngine;
using System.Collections;

public class ValueChanged : MonoBehaviour {

	public target_selector targetSel;

	public void DropdownChanged(int value)
	{
        GameControl.control.state.frame_markers[targetSel.target.GetComponentInParent<FrameMarkerController>().frame_marker_identifier].model = value;
	}
}
