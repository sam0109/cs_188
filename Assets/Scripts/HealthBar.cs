using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Vuforia;

public class HealthBar : MonoBehaviour {
    public Color minColor;
    public Color maxColor;
    public float minValue;
    public float maxValue;
    UnityEngine.UI.Image image_target;
    Actor parentFrameMarker;
    TrackableBehaviour marker;
	// Use this for initialization
	void Start () {
        image_target = gameObject.GetComponent<UnityEngine.UI.Image>();
        parentFrameMarker = GameControl.control.getActor(gameObject.GetComponentInParent<FrameMarkerController>().frame_marker_identifier);
        marker = gameObject.transform.parent.parent.GetComponent<MarkerBehaviour>();
    }
	
	// Update is called once per frame
	void Update () {

        if(marker.CurrentStatus != TrackableBehaviour.Status.TRACKED)
        {
            transform.localScale = Vector3.zero;
        }
        else
        {
            transform.localScale = new Vector3(parentFrameMarker.currentHealth / parentFrameMarker.maxHealth, transform.localScale.y, transform.localScale.z);
            image_target.color = Color.Lerp(minColor, maxColor, transform.localScale.x);
        }
    }
}
