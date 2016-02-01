using UnityEngine;
using System.Collections.Generic;

public class FrameMarkerGenerator : MonoBehaviour {
    public int marker_count;
    public GameObject frame_marker;
    List<GameObject> marker_list;
	// Use this for initialization
	void Start () {
	    for (int i = 0; i < marker_count; i++)
        {
            marker_list.Add(Instantiate(frame_marker));
			marker_list [i].GetComponent<Vuforia.MarkerBehaviour> ();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
