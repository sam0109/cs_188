using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;

public class GameControl : MonoBehaviour
{
    //if adding new values, don't forget to update setValues
    public List<int> frame_markers;
    public static GameControl control;
    public string mode;
    public TurnBasedMatch match;
    public bool canPlay;
    public string dm;

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

    public void setMode(string newMode)
    {
        mode = newMode;
    }

    public void setValues(GameControl new_control)
    {
        frame_markers = new_control.frame_markers;
        mode = new_control.mode;
    }
}
