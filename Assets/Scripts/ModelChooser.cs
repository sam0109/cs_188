using UnityEngine;
using System.Collections.Generic;

public class ModelChooser : MonoBehaviour
{
    public List<GameObject> models;
    GameObject current_model;
    int current_model_num;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if(current_model_num != GameControl.control.playerCharacter)
        {
            UpdateModel();
        }
    }

    void UpdateModel()
    {
        if(current_model)
        {
            Destroy(current_model);
        }
        current_model = (GameObject) Instantiate(models[GameControl.control.playerCharacter], Vector3.zero, Quaternion.identity);
        current_model_num = GameControl.control.playerCharacter;
        current_model.transform.SetParent(gameObject.transform, false);
        GetComponent<character_controller>().animate = current_model.GetComponent<Animation>();
    }
}