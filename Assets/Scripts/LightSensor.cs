using UnityEngine;
using System.Collections;

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
    }
	
	void Update ()
    {
	
	}

    public string getPlatformType()
    {
        return platform;
    }
}
