using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonIdentifier : MonoBehaviour {

    public void pressed()
    {
        gameObject.SendMessageUpwards("ButtonPressed", gameObject, SendMessageOptions.DontRequireReceiver);
    }
}
