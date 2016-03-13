using UnityEngine;
using System.Collections;

public class DMActions : MonoBehaviour {
    public FrameMarkerController selectedFrameMarker;
    target_selector targeter;

	// Use this for initialization
	void Start () {
        targeter = GameObject.FindGameObjectWithTag("Targeter").GetComponent<target_selector>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ButtonPressed()
    {
        if(selectedFrameMarker == null)
        {
            selectedFrameMarker = targeter.target.GetComponentInParent<FrameMarkerController>();
            GameControl.control.PopupMessage("Selected " + GameControl.control.rev_model_lookup[GameControl.control.state.frame_markers[selectedFrameMarker.frame_marker_identifier].model]);
        }
        else
        {
            selectedFrameMarker.Action();
            selectedFrameMarker = null;
        }
    }
}
