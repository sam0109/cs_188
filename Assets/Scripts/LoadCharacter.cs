using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class LoadCharacter : MonoBehaviour
{
    public Dropdown dropdown;

    public void startGame()
    {
        if(dropdown.captionText.text != "No Character")
        {
            GameControl.control.mode = "Player";

            string path = Path.Combine(Application.persistentDataPath, dropdown.options[dropdown.value].text + ".char");

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            Character obj = (Character)formatter.Deserialize(stream);
            stream.Close();

            GameControl.control.myCharacter = obj;
            GameControl.control.AcceptFromInbox();
        }
    }
}
