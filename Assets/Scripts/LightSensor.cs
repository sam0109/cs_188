using UnityEngine;
using System.Collections;
using UnityEngine.iOS;
using UnityEngine.UI;

public class LightSensor : MonoBehaviour
{
    private string platform;
    AndroidJavaClass androidClass;
    double sensorValue;
    public Text found;
    public Text light;

    void Start ()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            platform = "android";
            AndroidJNI.AttachCurrentThread();
            androidClass = new AndroidJavaClass("cksgame.cs188.com.lightsensor.MainActivity");
            object[] args = new[] { this.gameObject.name, "checkSensor" };
            androidClass.Call("initSensor", args);
            androidClass.Call("startSensor");
        }
        else if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            platform = "ios";
        }

    }

    public void checkSensor(string message)
    {
        if (message == "Update Sensor")
        {
            sensorValue = androidClass.CallStatic<double>("getSensorValue");
            light.text = "" + sensorValue;
        }
        else if(message == "Sensor Exists")
        {
            found.text = "FOUND!";
        }
        else if(message == "Sensor Not Found")
        {
            found.text = "DNE";
        }
    }

    public string getPlatformType()
    {
        return platform;
    }
}
