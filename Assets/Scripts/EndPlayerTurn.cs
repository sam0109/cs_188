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
        GameControl.control.TakeTurn(GameControl.control.state.dm);
    }

    void Update()
    {
        if (GameControl.control.isMyTurn && !nextTurn.gameObject.activeSelf)
        {
            nextTurn.gameObject.SetActive(true);
        }
        else if (!GameControl.control.isMyTurn && nextTurn.gameObject.activeSelf)
        {
            nextTurn.gameObject.SetActive(false);
        }
    }
}
