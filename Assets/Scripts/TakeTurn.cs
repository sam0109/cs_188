using UnityEngine;
using System.Collections;

public class TakeTurn : MonoBehaviour {
    MultiplayerFunctions multiplayer;

	// Use this for initialization
	void Start () {
	    multiplayer = GameObject.FindGameObjectWithTag("GameControl").GetComponent<MultiplayerFunctions>();
    }
	
	// Update is called once per frame
	public void TakeTurnPassthrough()
    {
        multiplayer.TakeTurn();
    }
}
