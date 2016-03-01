using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour {
	FrameMarkerController parentTakeDamage;
	Text message;

	// Use this for initialization
	void Start () {
		parentTakeDamage = gameObject.GetComponentInParent<FrameMarkerController>();
		message = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		/*if (message.text != parentTakeDamage.damageText) {
			message.text = parentTakeDamage.damageText;
		}*/
	}
}
