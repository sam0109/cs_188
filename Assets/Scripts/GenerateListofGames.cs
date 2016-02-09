using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GenerateListofGames : MonoBehaviour 
{
	public Dropdown dropdown;

    void Start()
    {
		dropdown.options.Clear ();
		string[] files = System.IO.Directory.GetDirectories(Application.dataPath + "/Models/Characters");

		for (int i = 0; i < files.Length; i++) 
		{
			Dropdown.OptionData option = new Dropdown.OptionData () { text = new System.IO.DirectoryInfo (files [i]).Name };
			dropdown.options.Add (option);
		}

		dropdown.value = 1;
		dropdown.value = 0;
    }

	public void SetGAMECONTROL()
	{
		GameControl.control.playerCharacter = dropdown.itemText.text;
	}
}
