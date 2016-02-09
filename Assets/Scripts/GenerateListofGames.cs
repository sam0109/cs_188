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
			files[i] = new System.IO.DirectoryInfo(files[i]).Name ;
			dropdown.options.Add (new Dropdown.OptionData () { text = files[i]});
		}

		dropdown.value = 1;
		dropdown.value = 0;
    }

	public void SetGAMECONTROL()
	{
		GameControl.control.playerCharacter = dropdown.itemText.name;
	}
}
