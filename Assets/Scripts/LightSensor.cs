using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LightSensor : MonoBehaviour
{
    public Button btn;
    public Light light;

    private bool sensorOn = false;

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
            float lightValue = Sensor.light;

            RenderSettings.ambientIntensity = lightValue;
            light.intensity = lightValue;
        }
    }
}
