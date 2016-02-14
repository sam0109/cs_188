using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine.SceneManagement;
using System;
using GooglePlayGames;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Vuforia;
using GooglePlayGames.BasicApi;

public class GameControl : MonoBehaviour
{
    //if adding new values, don't forget to update setValues
    public List<int> frame_markers;
    public static GameControl control;
    public GameState state;
    public string mode;
    public TurnBasedMatch match;
    public bool canPlay;
    public string dm;
	public int playerCharacter;

    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("logged in properly");
            }
            else
            {
                Debug.Log("did not log in properly");
            }
        });
    }

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        state.frame_markers = frame_markers;
    }

    public void setPlayerModel(int i)
    {
        control.playerCharacter = i;
    }

    public void setMode(string newMode)
    {
        mode = newMode;
    }

    public void setValues(GameState new_state)
    {
        frame_markers = new_state.frame_markers;
    }

    public Dictionary<string, string> GetPlayers()
    {
        Dictionary<string, string> players = new Dictionary<string, string>();
        foreach (Participant p in match.Participants)
        {
            if (p.ParticipantId != dm)
            {
                players.Add(p.Player.userName, p.ParticipantId);
                Debug.Log("Play Unity adding player " + p.Player.userName);
            }
        }
        return players;
    }

    public void CreateWithInvitationScreen()
    {
        if (Application.isEditor)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            const int MinPlayers = 1;
            const int MaxPlayers = 7;
            const int Variant = 0;  // default
            PlayGamesPlatform.Instance.TurnBased.CreateWithInvitationScreen(MinPlayers, MaxPlayers, Variant, OnMatchStarted);
        }
    }

    public void AcceptFromInbox()
    {
        if (!Application.isEditor)
        {
            PlayGamesPlatform.Instance.TurnBased.AcceptFromInbox(OnMatchStarted);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    // Callback:
    void OnMatchStarted(bool success, TurnBasedMatch new_match)
    {
        if (success)
        {
            // get the match data
            if (new_match.Data != null && new_match.Data.Length > 0)
            {
                setValues((GameState)ByteArrayToObject(new_match.Data));
            }
            match = new_match;
            canPlay = (new_match.Status == TurnBasedMatch.MatchStatus.Active &&
                    new_match.TurnStatus == TurnBasedMatch.MatchTurnStatus.MyTurn);
            if (mode == "Master")
            {
                dm = new_match.SelfParticipantId;
            }
            SceneManager.LoadScene(1);
        }
        else {
            Debug.Log("error starting match");
        }
    }

    public bool TakeTurn(string next)
    {
        if (canPlay)
        {
            byte[] myData = ObjectToByteArray(state);

            PlayGamesPlatform.Instance.TurnBased.TakeTurn(match, myData, next, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Sucessfully set turn play");
                }
                else {
                    Debug.Log("failed to set turn play");
                }
            });
            return true;
        }
        else
        {
            Debug.Log("Not my turn play");
            return false;
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

[System.Serializable]
public class GameState
{
    public List<int> frame_markers;
    public List<Character> Characters;
}

[System.Serializable]
public class Character
{
    public string name;
    public string player;
    public int model;
}