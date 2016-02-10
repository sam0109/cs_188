using UnityEngine;
using System.Collections;

public class EndPlayerTurn : MonoBehaviour {

	public void EndTurn()
    {
        GameControl.control.TakeTurn(GameControl.control.dm);
    }
}
