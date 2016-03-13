using UnityEngine;
using Vuforia;
using System.Collections;

public class hideWhenDeselected : MonoBehaviour {
    FrameMarkerController parentFrameMarker;
	// Use this for initialization
	void Start () {
        parentFrameMarker = gameObject.GetComponentInParent<FrameMarkerController>();
    }
	
	// Update is called once per frame
	void Update () {
	if(parentFrameMarker.marker.CurrentStatus != TrackableBehaviour.Status.TRACKED)
        {
            Destroy(gameObject);
        }
	}
}
