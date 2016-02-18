using UnityEngine;
using System.Collections;
using Vuforia;

public class SetAutoFocus : MonoBehaviour {
#if UNITY_ANDROID
    void Start()
    {
        VuforiaAbstractBehaviour vuforiaBehaviour = (VuforiaAbstractBehaviour)FindObjectOfType(typeof(VuforiaAbstractBehaviour));
        if (vuforiaBehaviour)
        {
            vuforiaBehaviour.RegisterVuforiaStartedCallback(EnableContinuousAutoFocus);
            vuforiaBehaviour.RegisterOnPauseCallback(OnPause);
        }
    }

    private void EnableContinuousAutoFocus()
    {
        if (!CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO))
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
        }
    }

    private void OnPause(bool pause)
    {
        if (!pause)
        {
            // set to continous autofocus
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
    }
#endif
}