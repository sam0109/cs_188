using UnityEngine;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MultiplayerFunctions : MonoBehaviour
{
    public GameControl control;

    public void CreateWithInvitationScreen()
    {
        if(Application.isEditor)
        {
            SceneManager.LoadScene(1);
        }
        
        const int MinPlayers = 1;
        const int MaxPlayers = 7;
        const int Variant = 0;  // default
        PlayGamesPlatform.Instance.TurnBased.CreateWithInvitationScreen(MinPlayers, MaxPlayers, Variant, OnMatchStarted);
    }

    public void AcceptFromInbox()
    {
        PlayGamesPlatform.Instance.TurnBased.AcceptFromInbox(OnMatchStarted);
    }

    // Callback:
    void OnMatchStarted(bool success, TurnBasedMatch new_match)
    {
        if (success)
        {
            // get the match data
            if (new_match.Data != null && new_match.Data.Length > 0)
            {
                control.setValues((GameState)ByteArrayToObject(new_match.Data));
            }
            control.match = new_match;
            control.canPlay = (new_match.Status == TurnBasedMatch.MatchStatus.Active &&
                    new_match.TurnStatus == TurnBasedMatch.MatchTurnStatus.MyTurn);
            if (GameControl.control.mode == "Master")
            {
                control.dm = new_match.SelfParticipantId;
            }
            SceneManager.LoadScene(1);
        }
        else {
            Debug.Log("error starting match");
        }
    }

    public void TakeTurn()
    {
        if (control.canPlay)
        {
            byte[] myData = ObjectToByteArray(control.state);
            string nextPlayer = control.dm;
            if (control.dm == control.match.SelfParticipantId)
            {
                foreach(Participant participant in control.match.Participants)
                {
                    if(participant.ParticipantId != control.match.SelfParticipantId)
                    {
                        nextPlayer = participant.ParticipantId;
                    }
                }
                
            }
            PlayGamesPlatform.Instance.TurnBased.TakeTurn(control.match, myData, nextPlayer, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Sucessfully set turn play");
                }
                else {
                    Debug.Log("failed to set turn play");
                }
            });
        }
        else
        {
            Debug.Log("Not my turn play");
        }
    }

    public static byte[] ObjectToByteArray(System.Object obj)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (var ms = new MemoryStream())
        {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }

    public static System.Object ByteArrayToObject(byte[] arrBytes)
    {
        using (var memStream = new MemoryStream())
        {
            var binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            var obj = binForm.Deserialize(memStream);
            return obj;
        }
    }
}