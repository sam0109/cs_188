using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GenerateListofGames : MonoBehaviour 
{
	public Dropdown dropdown;


    void Start()
    {
		/*
		dropdown.options.Clear ();
		string[] files = System.IO.Directory.GetDirectories(Application.persistentDataPath + "/Models/Characters");

		for (int i = 0; i < files.Length; i++) 
		{
			Dropdown.OptionData option = new Dropdown.OptionData () { text = new System.IO.DirectoryInfo (files [i]).Name };
			dropdown.options.Add (option);
		}
		*/
    }

	public void SetGameControl()
	{
        GameControl.control.playerCharacter = dropdown.value;
	}
}
