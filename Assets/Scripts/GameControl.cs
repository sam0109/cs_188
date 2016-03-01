﻿using UnityEngine;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GooglePlayGames.BasicApi;

public class GameControl : MonoBehaviour
{
    public static GameControl control;
    public TurnBasedMatch match;
    public GameState state;
    public string mode;
    public Actor myCharacter;
    public string playerID;
	public bool isMyTurn;
    public int numMarkers;

    public List<GameObject> models;
    public List<string> model_names;
    public Dictionary<string, int> model_lookup;
    public List<FrameMarkerController> frame_markers;
    public GameObject highlighted;

    void Start()
    {
        frame_markers = new List<FrameMarkerController>();
        model_lookup = new Dictionary<string, int>();
        for(int i = 0; i < model_names.Count; i++)
        {
            model_lookup.Add(model_names[i], i);
        }
        numMarkers = 10;
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()

        // registers a callback for turn based match notifications.
        .WithMatchDelegate(OnGotMatch)

        .Build();

        PlayGamesPlatform.InitializeInstance(config);

        // recommended for debugging:
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

    private void OnGotMatch(TurnBasedMatch new_match, bool shouldAutoLaunch)
    {
        // get the match data
        if (new_match.Data != null && new_match.Data.Length > 0)
        {
            setValues((GameState)ByteArrayToObject(new_match.Data));
        }
        else
        {
            state = new GameState();
            state.frame_markers = new List<Actor>();
        }

        match = new_match;
        playerID = match.SelfParticipantId;

        if (mode == "Player")
        {
            for (int i = 0; i < state.frame_markers.Count; i++)
            {
                if (state.frame_markers[i].player == playerID)
                {
                    state.frame_markers[i] = myCharacter;
                }
            }
        }
        if (SceneManager.GetActiveScene().buildIndex < 1)
        {
            SceneManager.LoadScene(1);
        }
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

    public void setValues(GameState new_state)
    {
        state = new_state;
    }

    public void updateModel(int frameMarker, string model)
    {
        state.frame_markers[frameMarker].model = model_lookup[model];
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
            state.frame_markers = new List<Actor>();
            for (int i = 0; i < numMarkers; i++)
            {
                state.frame_markers.Add(new Actor(0, state.dm));
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
            state.frame_markers = new List<Actor>();
            for (int i = 0; i < numMarkers; i++)
            {
                state.frame_markers.Add(new Actor(0, state.dm));
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
                state.frame_markers = new List<Actor>();
            }
            match = new_match;
            playerID = match.SelfParticipantId;
            if (mode == "Master")
            {
                state = new GameState();
                state.dm = playerID;
                state.frame_markers = new List<Actor>();
                for(int i = 0; i < numMarkers; i++)
                {
                    state.frame_markers.Add(new Actor(0, state.dm));
                }

            }
            if (mode == "Player")
            {
                for(int i = 0; i < state.frame_markers.Count; i++)
                {
                    if(state.frame_markers[i].player == playerID)
                    {
                        state.frame_markers[i] = myCharacter;
                    }
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
        if (isMyTurn)
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
    public List<Actor> frame_markers;
}
	

[System.Serializable]
public class Actor
{
    public Actor()
    {
        return;
    }

    public Actor(int new_model, string new_player)
    {
        model = new_model;
        player = new_player;
    }

    public bool isPlayer;
    public int model;
    public string player;
    public string characterName;
    public string characterClass;
    public string characterRace;
    public int str, dex, con, intelligence, wiz, cha;
    public int maxHealth;
    public int currentHealth;
    public int range;
}