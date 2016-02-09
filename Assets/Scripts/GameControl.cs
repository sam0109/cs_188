using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;


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
	public string playerCharacter;


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

    public void setMode(string newMode)
    {
        mode = newMode;
    }

    public void setValues(GameState new_state)
    {
        frame_markers = new_state.frame_markers;
    }
}

[System.Serializable]
public class GameState
{
    public List<int> frame_markers;
}
