using UnityEngine;
using System.Collections;

public class hideFromPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    if(!Application.isEditor && GameControl.control.getDM() != GameControl.control.myself.ParticipantId)
        {
            gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
