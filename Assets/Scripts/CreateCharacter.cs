using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class CreateCharacter : MonoBehaviour {

    public InputField characterName;
    public Dropdown characterClass;
    public Dropdown characterRace;
    public InputField str;
    public InputField dex;
    public InputField con;
    public InputField intelligence;
    public InputField wiz;
    public InputField cha;
    public InputField lvl;

    public void SaveCharacter ()
    {
        Actor newCharacter = new Actor();

        newCharacter.characterName = characterName.text;
        newCharacter.isPlayer = true;
        newCharacter.player = "";
        newCharacter.model = 5;
        newCharacter.characterClass = characterClass.options[characterClass.value].text;
        newCharacter.characterRace = "";

        try
        {
            newCharacter.str = int.Parse(str.text);
        }
        catch (FormatException)
        {
            newCharacter.str = 8;
        }

        try
        {
            newCharacter.dex = int.Parse(dex.text);
        }
        catch (FormatException)
        {
            newCharacter.dex = 8;
        }

        try
        {
            newCharacter.con = int.Parse(con.text);
        }
        catch (FormatException)
        {
            newCharacter.con = 8;
        }

        try
        {
            newCharacter.intelligence = int.Parse(intelligence.text);
        }
        catch (FormatException)
        {
            newCharacter.intelligence = 8;
        }

        try
        {
            newCharacter.wiz = int.Parse(wiz.text);
        }
        catch (FormatException)
        {
            newCharacter.wiz = 8;
        }

        try
        {
            newCharacter.cha = int.Parse(cha.text);
        }
        catch (FormatException)
        {
            newCharacter.cha = 8;
        }

        try
        {
            newCharacter.level = int.Parse(lvl.text);
        }
        catch(FormatException)
        {
            newCharacter.level = 1;
        }

        newCharacter.hat = GameControl.control.myCharacter.hat;
        newCharacter.hairStyle = GameControl.control.myCharacter.hairStyle;
        newCharacter.hairColor = GameControl.control.myCharacter.hairColor;
        newCharacter.body = GameControl.control.myCharacter.body;
        newCharacter.clothes = GameControl.control.myCharacter.clothes;
        newCharacter.beard = GameControl.control.myCharacter.beard;

        newCharacter.maxHealth = (Mathf.Max(newCharacter.level, 1)) * Mathf.Max(((newCharacter.con / 2) - 5), 1);
        newCharacter.currentHealth = newCharacter.maxHealth;

        IFormatter formatter = new BinaryFormatter();

        string path = Path.Combine(Application.persistentDataPath, newCharacter.characterName + ".char");
        
        Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, newCharacter);
        stream.Close();
    }
}
