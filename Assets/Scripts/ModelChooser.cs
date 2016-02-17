using UnityEngine;
using System.Collections.Generic;

public class ModelChooser : MonoBehaviour
{
    public List<GameObject> models;
    GameObject current_model;
    int current_model_num;

    void Start()
    {
        current_model_num = -1;
    }

    void Update()
    {
        if (GameControl.control.state != null &&
            GameControl.control.state.Characters != null &&
            GameControl.control.state.Characters.ContainsKey(gameObject.transform.parent.GetComponent<FrameMarkerController>().owner))
        {
            if (current_model_num != GameControl.control.state.Characters[gameObject.transform.parent.GetComponent<FrameMarkerController>().owner].model)
            {
                UpdateModel(GameControl.control.state.Characters[gameObject.transform.parent.GetComponent<FrameMarkerController>().owner].model);
            }
        }
        else
        {
            if (current_model_num != GameControl.control.playerCharacter)
            {
                UpdateModel(GameControl.control.playerCharacter);
            }
        }
    }

    void UpdateModel(int new_model)
    {
        if(current_model)
        {
            Destroy(current_model);
        }
        current_model = Instantiate(models[new_model]);
        current_model_num = new_model;
        current_model.transform.SetParent(gameObject.transform, false);
        GetComponent<character_controller>().animate = current_model.GetComponent<Animation>();
    }
}