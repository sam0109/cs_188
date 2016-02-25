using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class FrameMarkerController : MonoBehaviour {
    public int frame_marker_identifier;
    public string owner;
    target_selector targeter;
    GameObject particles;
    bool particlesPlaying;
    int current_model_num;
    GameObject current_model;
    Vuforia.MarkerBehaviour marker;

    public void Start()
    {
        particlesPlaying = false;
        current_model_num = -1;
        targeter = GameObject.FindGameObjectWithTag("Targeter").GetComponent<target_selector>();
        owner = "";
        marker = gameObject.GetComponent<Vuforia.MarkerBehaviour>();
        frame_marker_identifier = marker.Marker.MarkerID;
    }

    public void SetModel(int i)
    {
        if (i >= 0 && i < GameControl.control.models.Count)
        {
            if (current_model)
            {
                Destroy(current_model);
            }
            current_model = Instantiate(GameControl.control.models[i]);
            current_model.transform.SetParent(gameObject.transform, false);
        }
        else
        {
            Debug.Log(i + " is out of range");
        }
    }

    public void Update()
    {
        if (marker.CurrentStatus == Vuforia.TrackableBehaviour.Status.TRACKED)
        {
            if (current_model_num != GameControl.control.state.frame_markers[frame_marker_identifier].model)
            {
                current_model_num = GameControl.control.state.frame_markers[frame_marker_identifier].model;
                SetModel(current_model_num);
            }
            if (GameControl.control.state.frame_markers[frame_marker_identifier].model == 1)
            {
                owner = GameControl.control.state.frame_markers[frame_marker_identifier].player;
            }

            if (targeter.target == current_model)
            {
                if (!particlesPlaying)
                {
                    particlesPlaying = true;
                    particles = Instantiate(GameControl.control.highlighted);
                    particles.transform.SetParent(gameObject.transform, false);
                }
            }
            else if (particlesPlaying)
            {
                particlesPlaying = false;
                Destroy(particles);
            }
        }
        else
        {
            if (current_model)
            {
                Destroy(current_model);
                current_model_num = -1;
            }
        }
    }
}
