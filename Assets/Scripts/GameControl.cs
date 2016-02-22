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
using System.Linq;

public class GameControl : MonoBehaviour
{
    public static GameControl control;
    public GameState state;
    public string mode;
    public TurnBasedMatch match;
	public int playerCharacter;
    public string playerID;
    public Character myCharacter;
	public bool isMyTurn;
    public int numMarkers;
    public List<GameObject> models;
    public GameObject highlighted;

    void Start()
    {
        numMarkers = 10;
        playerCharacter = 0;
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
        if (match != null)
        {
            if (match.Status == TurnBasedMatch.MatchStatus.Active &&
                match.TurnStatus == TurnBasedMatch.MatchTurnStatus.MyTurn)
            {
                isMyTurn = true;
            }
            else
            {
                isMyTurn = false;
            }
        }
	}

    public void setMode(string newMode)
    {
        mode = newMode;
    }

    public void setPlayerModel(int model)
    {
        playerCharacter = model;
    }

    public void setValues(GameState new_state)
    {
        state = new_state;
    }

    public Dictionary<string, string> GetPlayers()
    {
        Dictionary<string, string> players = new Dictionary<string, string>();
        if (Application.isEditor)
        {
            return players;
        }
            foreach (Participant p in match.Participants)
        {
            if (p.ParticipantId != state.dm)
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
            state = new GameState();
            state.dm = "meee";
            state.frame_markers = new List<model_player>();
            for (int i = 0; i < numMarkers; i++)
            {
                state.frame_markers.Add(new model_player(0, state.dm));
            }
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
        if (Application.isEditor)
        {
            state = new GameState();
            state.dm = "meee";
            state.frame_markers = new List<model_player>();
            for (int i = 0; i < numMarkers; i++)
            {
                state.frame_markers.Add(new model_player(0, state.dm));
            }
            SceneManager.LoadScene(1);
        }
        else
        {
            PlayGamesPlatform.Instance.TurnBased.AcceptFromInbox(OnMatchStarted);
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
            else
            {
                state = new GameState();
                state.frame_markers = new List<model_player>();
                state.Characters = new Dictionary<string, Character>();
            }
            match = new_match;
            playerID = match.SelfParticipantId;
            if (mode == "Master")
            {
                state = new GameState();
                state.dm = playerID;
                state.frame_markers = new List<model_player>();
                for(int i = 0; i < numMarkers; i++)
                {
                    state.frame_markers.Add(new model_player(0, state.dm));
                }
                state.Characters = new Dictionary<string, Character>();

            }
            if (mode == "Player")
            {
                if(!state.Characters.ContainsKey(playerID))
                {
                    state.Characters.Add(playerID, myCharacter);
                }
                else
                {
                    state.Characters[playerID] = myCharacter;
                }
            }
            SceneManager.LoadScene(1);
        }
        else {
            Debug.Log("error starting match");
        }
    }

    public bool TakeTurn(string next)
    {
        if (match.Status == TurnBasedMatch.MatchStatus.Active &&
                    match.TurnStatus == TurnBasedMatch.MatchTurnStatus.MyTurn)
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
    public string dm;
    public List<model_player> frame_markers;
    public Dictionary<string, Character> Characters;
}

[System.Serializable]
public class Character
{
    public string characterName;
    public string player;
    public int model;
    public string characterClass;
    public string characterRace;
    public int str, dex, con, intelligence, wiz, cha;
}

[System.Serializable]
public class model_player
{
    public model_player(int new_model, string new_player)
    {
        model = new_model;
        player = new_player;
    }
    public int model;
    public string player;
}