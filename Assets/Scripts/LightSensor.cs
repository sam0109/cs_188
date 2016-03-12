using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LightSensor : MonoBehaviour
{
    public Button btn;
    public Light my_light;
    public float lightValue;

    public bool sensorOn = false;

    void Start()
    {
        //Sensor.Activate(Sensor.Type.Light);
        btn.onClick.AddListener(delegate { onSense(); });
    }

    public void onSense()
    {
        if(sensorOn == false)
        {
            if (!Application.isEditor)
            {
                sensorOn = Sensor.Activate(Sensor.Type.Light);
            }
            else
            {
                sensorOn = true;
            }
        }
        else
        {
            sensorOn = false;

            if (!Application.isEditor)
            {
                Sensor.Deactivate(Sensor.Type.Light);
            }
        }
    }

    public void Update()
    {
        if (sensorOn)
        {
            if (!Application.isEditor)
            {
                lightValue = Mathf.Clamp(Sensor.light / 110000.0f,0,1);
                Debug.Log("Light value from sensor: " + lightValue.ToString());
            }

            RenderSettings.ambientIntensity = lightValue;
            my_light.intensity = lightValue;
        }
    }
}
