using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChooseCanvas : MonoBehaviour 
{
    public GameObject master;
    public GameObject player;

    bool first;

    void OnLevelWasLoaded(int level)
    {
        first = false;
    }

	void OnGUI()
    {
        if (SceneManager.GetActiveScene().name == "main")
        {
            if (GameControl.control.mode == "Master")
            {
                master.SetActive(true);
                player.SetActive(false);
            }
            else if (GameControl.control.mode == "Player")
            {
                master.SetActive(false);
                player.SetActive(true);
            }
        }
    }
}
