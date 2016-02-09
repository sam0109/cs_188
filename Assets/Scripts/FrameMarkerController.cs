using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class FrameMarkerController : MonoBehaviour {
    public List<GameObject> models;
    public Dropdown DropdownButton;
    public GameControl gameController;
    public target_selector targeter;
    public int frame_marker_identifier;
    public GameObject highlighted;
    public GameObject particles;
    public bool particlesPlaying;
    int current_model_num;
    GameObject current_model;

    public void Start()
    {
        particlesPlaying = false;
        current_model_num = -1;
        gameController = GameObject.FindGameObjectWithTag("GameControl").GetComponent<GameControl>();
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
            print(i + " is out of range");
        }
    }

    public void Update()
    {

        if (current_model_num != gameController.frame_markers[frame_marker_identifier])
        {
            current_model_num = gameController.frame_markers[frame_marker_identifier];
            SetModel(current_model_num);
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
