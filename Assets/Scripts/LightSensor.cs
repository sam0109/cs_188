using UnityEngine;
using System.Collections;
using UnityEngine.iOS;

public class LightSensor : MonoBehaviour
{
    private string platform;

	void Start ()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            platform = "android";
        }
        else if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            platform = "ios";
        }
        AndroidJavaClass cls = new AndroidJavaClass("android.hardware.SensorManager");
    }

    public string getPlatformType()
    {
        return platform;
    }
}
