using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour 
{
    public static GameControl control;

    public string mode;

	void Awake () 
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if(control != this)
        {
            Destroy(gameObject);
        }
	}

    public void setMode(string newMode)
    {
        mode = newMode;
    }
}
