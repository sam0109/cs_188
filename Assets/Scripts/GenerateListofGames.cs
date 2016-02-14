using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class GenerateListofGames : MonoBehaviour 
{
	public Dropdown dropdown;


    void OnEnable()
    {
		dropdown.options.Clear ();
		string[] files = System.IO.Directory.GetFiles(Application.persistentDataPath, "*.char");

		for (int i = 0; i < files.Length; i++) 
		{
			Dropdown.OptionData option = new Dropdown.OptionData () { text = Path.GetFileNameWithoutExtension(new System.IO.DirectoryInfo (files [i]).Name) };
			dropdown.options.Add (option);
		}

        if(dropdown.options.Count == 0)
        {
            dropdown.captionText.text = "No Character";
        }
        else
        {
            dropdown.value = 1;
            dropdown.value = 0;
        }
    }

	public void SetGameControl()
	{
        GameControl.control.playerCharacter = dropdown.value;
	}
}
