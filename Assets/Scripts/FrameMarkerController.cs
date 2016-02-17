﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class FrameMarkerController : MonoBehaviour {
    public List<GameObject> models;
    public int frame_marker_identifier;
    public GameObject highlighted;
    public string owner;
    target_selector targeter;
    GameObject particles;
    bool particlesPlaying;
    int current_model_num;
    GameObject current_model;

    public void Start()
    {
        particlesPlaying = false;
        current_model_num = -1;
        targeter = GameObject.FindGameObjectWithTag("Targeter").GetComponent<target_selector>();
        owner = "";
        //fix once frame markers are dynamic
    }

    public void SetModel(int i)
    {
        if (i >= 0 && i < models.Count)
        {
            if (current_model)
            {
                Destroy(current_model);
            }
            current_model = Instantiate(models[i]);
            current_model.transform.SetParent(gameObject.transform, false);
        }
        else
        {
            Debug.Log(i + " is out of range");
        }
    }

    public void Update()
    {
        if (current_model_num != GameControl.control.state.frame_markers[frame_marker_identifier].model)
        {
            current_model_num = GameControl.control.state.frame_markers[frame_marker_identifier].model;
            SetModel(current_model_num);
        }
        if(GameControl.control.state.frame_markers[frame_marker_identifier].model == 1)
        {
            owner = GameControl.control.state.frame_markers[frame_marker_identifier].player;
        }

        if(targeter.target == current_model)
        {
            if (!particlesPlaying)
            {
                particlesPlaying = true;
                particles = Instantiate(highlighted);
                particles.transform.SetParent(gameObject.transform, false);
            }
        }
        else if (particlesPlaying)
        {
            particlesPlaying = false;
            Destroy(particles);
        }
    }
}
