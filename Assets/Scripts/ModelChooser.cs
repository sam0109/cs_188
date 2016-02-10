using UnityEngine;
using System.Collections.Generic;

public class ModelChooser : MonoBehaviour
{
    public List<GameObject> models;
    public GameControl gameController;
    GameObject current_model;

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameControl").GetComponent<GameControl>();
        switch (gameController.playerCharacter)
        {
            case "FemaleWarrior":
                current_model = Instantiate(models[0]);
                break;
            case "Magician":
                current_model = Instantiate(models[1]);
                break;
            case "Samurai":
                current_model = Instantiate(models[2]);
                break;
            case "SpartanKing":
                current_model = Instantiate(models[3]);
                break;
            default:
                break;
        }
        current_model.transform.SetParent(gameObject.transform, false);
    }
}