using UnityEngine;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MultiplayerFunctions : MonoBehaviour
{
    public GameControl control;
    TurnBasedMatch match;

    public void CreateWithInvitationScreen()
    {
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
            match = new_match;
            LoadTurn();
            control.dm = match.SelfParticipantId;
            SceneManager.LoadScene(1);
        }
        else {
            Debug.Log("error starting match");
        }
    }

    void LoadTurn()
    {
        if(match.Data != null && match.Data.Length > 0)
        {
            control.setValues((GameControl)ByteArrayToObject(match.Data));
        }
        control.match = match;
        control.canPlay = (match.Status == TurnBasedMatch.MatchStatus.Active &&
                match.TurnStatus == TurnBasedMatch.MatchTurnStatus.MyTurn);
    }

    public void TakeTurn()
    {
        if (control.canPlay)
        {
            byte[] myData = ObjectToByteArray(control);
            string nextPlayer = control.dm;
            if (control.dm == match.SelfParticipantId)
            {
                foreach(Participant participant in match.Participants)
                {
                    if(participant.ParticipantId != match.SelfParticipantId)
                    {
                        nextPlayer = participant.ParticipantId;
                    }
                }
                
            }
            PlayGamesPlatform.Instance.TurnBased.TakeTurn(match, myData, "p_2", (bool success) =>
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