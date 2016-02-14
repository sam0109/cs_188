using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class CreateCharacter : MonoBehaviour {

    public Dropdown model;
    public InputField name;

    public void SaveCharacter ()
    {
        Character newCharater = new Character();

        newCharater.name = name.text;
        newCharater.player = model.options[model.value].text;
        newCharater.model = model.value;

        IFormatter formatter = new BinaryFormatter();

        string path = Path.Combine(Application.persistentDataPath, newCharater.name + ".char");
        
        Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, newCharater);
        stream.Close();
    }
}
