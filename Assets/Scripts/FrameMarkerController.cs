using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class FrameMarkerController : MonoBehaviour {
    public List<GameObject> models;
    public Dropdown DropdownButton;
    public GameControl gameController;
    public int frame_marker_identifier;
    int current_model_num;
    GameObject current_model;

    public void Start()
    {
        current_model_num = -1;
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
        //print(current_model_num);
        print(gameController);

        if (current_model_num != gameController.frame_markers[frame_marker_identifier])
        {
            current_model_num = gameController.frame_markers[frame_marker_identifier];
            SetModel(current_model_num);
        }
    }
}
