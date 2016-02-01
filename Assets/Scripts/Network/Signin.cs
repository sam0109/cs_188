using UnityEngine;
using System.Collections;

public class Signin : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        MultiplayerController.Instance.TrySilentSignIn();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        MultiplayerController.Instance.SignInAndStartMPGame();
    }
}
