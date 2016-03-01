using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Color minColor;
    public Color maxColor;
    public float minValue;
    public float maxValue;
    Image image_target;
    FrameMarkerController parentFrameMarker;
	// Use this for initialization
	void Start () {
        image_target = gameObject.GetComponent<Image>();
        parentFrameMarker = gameObject.GetComponentInParent<FrameMarkerController>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(GameControl.control.state.frame_markers[parentFrameMarker.frame_marker_identifier].currentHealth / GameControl.control.state.frame_markers[parentFrameMarker.frame_marker_identifier].maxHealth, transform.localScale.y, transform.localScale.z);
        image_target.color = Color.Lerp(minColor, maxColor, transform.localScale.x);
    }
}
