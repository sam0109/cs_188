using UnityEngine;
using System.Collections;
using Vuforia;

public class SetAutoFocus : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
