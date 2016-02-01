using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class Signin : MonoBehaviour {
    public GameObject backdrop;
	// Use this for initialization
	void Start ()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate((bool success) => {
            if(success)
            {
                backdrop.SetActive(false);
                Debug.Log("logged in properly");
            }
            else
            {
                Debug.Log("did not log in properly");
            }
        });
    }
}
