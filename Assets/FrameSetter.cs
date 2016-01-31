using UnityEngine;
using System.Collections;

public class FrameSetter : MonoBehaviour {

	public target_selector targeter;
	public GameObject Drop;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (targeter.target) {
			Drop.SetActive (true);
		}
	
	}
		
}
