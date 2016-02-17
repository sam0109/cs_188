using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ButtonMaker : MonoBehaviour {
    public List<string> objects;
    public GameObject button;
    target_selector targetSel;
    List<GameObject> buttons;
    public GameObject playerSelector;
	// Use this for initialization
	void Start () {
        targetSel = GameObject.FindGameObjectWithTag("Targeter").GetComponent<target_selector>();
        for (int i = 0; i < objects.Count; i++)
        {
            GameObject temp_button = Instantiate(button);
            temp_button.transform.SetParent(gameObject.transform, false);
            temp_button.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * 30, 30);
            temp_button.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            temp_button.GetComponentInChildren<Text>().text = objects[i];
            temp_button.GetComponent<ButtonIdentifier>().buttonID = i;
        }
	}

    public void ButtonPressed(GameObject button)
    {
        if (button.GetComponent<ButtonIdentifier>().buttonID == 1)
        {
            playerSelector.SetActive(true);
            gameObject.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            GameControl.control.state.frame_markers[targetSel.target.GetComponentInParent<FrameMarkerController>().frame_marker_identifier].model = button.GetComponent<ButtonIdentifier>().buttonID;
        }
    }
}
