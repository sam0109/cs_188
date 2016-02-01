using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine.SceneManagement;

public class MultiplayerFunctions : MonoBehaviour
{
    public void CreateWithInvitationScreen()
    {
        const int MinOpponents = 1;
        const int MaxOpponents = 7;
        const int Variant = 0;  // default
        PlayGamesPlatform.Instance.TurnBased.CreateWithInvitationScreen(MinOpponents, MaxOpponents, Variant, OnMatchStarted);
    }
    public void AcceptFromInbox()
    {
        PlayGamesPlatform.Instance.TurnBased.AcceptFromInbox(OnMatchStarted);
    }

    // Callback:
    void OnMatchStarted(bool success, TurnBasedMatch match)
    {
        if (success)
        {
            SceneManager.LoadScene(1);
        }
        else {
            // show error message
        }
    }

}

