using UnityEngine;
using System.Collections.Generic;
using Vuforia;

public class FrameMarkerGenerator : MonoBehaviour {
    public int numMarkers;

    // Use this for initialization
    void Start () {
        MarkerTracker mt = TrackerManager.Instance.GetTracker<MarkerTracker>();
        for (int i = 0; i < numMarkers; i++)
        {
            MarkerAbstractBehaviour marker = mt.CreateMarker(i, "Marker" + i.ToString(), 100);
            marker.gameObject.AddComponent<FrameMarkerController>();
        }
    }
}
