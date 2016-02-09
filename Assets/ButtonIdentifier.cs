using UnityEngine;
using System.Collections;

public class ButtonIdentifier : MonoBehaviour {
    public int buttonID;

    public void Pressed()
    {
        gameObject.SendMessageUpwards("ButtonPressed", buttonID);
    }
}
