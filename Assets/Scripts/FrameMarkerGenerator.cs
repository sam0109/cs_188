using UnityEngine;
using System.Collections.Generic;
using Vuforia;

public class FrameMarkerGenerator : MonoBehaviour {

    // Use this for initialization
    void Start () {
        MarkerTracker mt = TrackerManager.Instance.GetTracker<MarkerTracker>();
        for (int i = 0; i < GameControl.control.numMarkers; i++)
        {
            MarkerAbstractBehaviour marker = mt.CreateMarker(i, "Marker" + i.ToString(), 1);
            marker.gameObject.AddComponent<FrameMarkerController>();
            GameControl.control.frame_markers.Add(marker.gameObject.GetComponent<FrameMarkerController>());
        }
    }
}
