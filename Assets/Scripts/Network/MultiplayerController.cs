using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

public class MultiplayerController
{
    private static MultiplayerController _singleton = null;

    private MultiplayerController()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    public static MultiplayerController Instance
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = new MultiplayerController();
            }

            return _singleton;
        }
    }

    public void SignInAndStartMPGame()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.localUser.Authenticate((bool success) => 
            {
                if (success)
                {
                    Debug.Log("Signed in " + PlayGamesPlatform.Instance.localUser.userName);
                }
                else
                {
                    Debug.Log("Not in");
                }
            });
        }
        else
        {
            Debug.Log("Already signed in.");
        }
    }

    public void TrySilentSignIn()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.Authenticate((bool success) => 
            {
                if (success)
                {
                    Debug.Log("Silent in " + PlayGamesPlatform.Instance.localUser.userName);
                }
                else {
                    Debug.Log("not signed in");
                }
            }, true);
        }
        else {
            Debug.Log("already signed in");
        }
    }
}
