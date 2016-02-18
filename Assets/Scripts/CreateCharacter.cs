using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class CreateCharacter : MonoBehaviour {

    public Dropdown model;
    public InputField characterName;
    public Dropdown characterClass;
    public Dropdown characterRace;
    public InputField str;
    public InputField dex;
    public InputField con;
    public InputField intelligence;
    public InputField wiz;
    public InputField cha;

    public void SaveCharacter ()
    {
        Character newCharacter = new Character();

        newCharacter.characterName = characterName.text;
        newCharacter.player = "";
        newCharacter.model = model.value;
        newCharacter.characterClass = characterClass.options[characterClass.value].text;
        newCharacter.characterRace = characterRace.options[characterRace.value].text;

        try
        {
            newCharacter.str = int.Parse(str.text);
        }
        catch (FormatException)
        {
            newCharacter.str = 0;
        }

        try
        {
            newCharacter.dex = int.Parse(dex.text);
        }
        catch (FormatException)
        {
            newCharacter.dex = 0;
        }

        try
        {
            newCharacter.con = int.Parse(con.text);
        }
        catch (FormatException)
        {
            newCharacter.con = 0;
        }

        try
        {
            newCharacter.intelligence = int.Parse(intelligence.text);
        }
        catch (FormatException)
        {
            newCharacter.intelligence = 0;
        }

        try
        {
            newCharacter.wiz = int.Parse(wiz.text);
        }
        catch (FormatException)
        {
            newCharacter.wiz = 0;
        }

        try
        {
            newCharacter.cha = int.Parse(cha.text);
        }
        catch (FormatException)
        {
            newCharacter.cha = 0;
        }

        IFormatter formatter = new BinaryFormatter();

        string path = Path.Combine(Application.persistentDataPath, newCharacter.characterName + ".char");
        
        Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, newCharacter);
        stream.Close();
    }
}
