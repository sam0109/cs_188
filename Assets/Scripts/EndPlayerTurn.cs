using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GooglePlayGames.BasicApi.Multiplayer;

public class EndPlayerTurn : MonoBehaviour {
    Button nextTurn;

    void Start()
    {
        nextTurn = gameObject.GetComponent<Button>();
    }
    
	public void EndTurn()
    {
        GameControl.control.TakeTurn(GameControl.control.getDM());
    }

    void Update()
    {
        if (GameControl.control.isMyTurn && !nextTurn.interactable)
        {
            nextTurn.interactable = true;
        }
        else if (!GameControl.control.isMyTurn && nextTurn.interactable)
        {
            nextTurn.interactable = false;
        }
    }
}
