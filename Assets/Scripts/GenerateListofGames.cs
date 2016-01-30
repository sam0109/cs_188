using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GenerateListofGames : MonoBehaviour 
{
    public string ext;

    void OnBecameVisible()
    {
        string[] files = System.IO.Directory.GetFiles(Application.persistentDataPath, "*." + ext);
        ((Dropdown)(transform.root.gameObject.GetComponent<Dropdown>())).AddOptions(new List<string>(files));
    }
}
