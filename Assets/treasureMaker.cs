using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class treasureMaker : MonoBehaviour {
	public GameObject button;
	target_selector targetSel;
	List<GameObject> buttons;
	public GameObject objectSelector;
	List<string> treasures;
    static ItemDataBaseList inventoryItemList;

	void Start()
	{
		targetSel = GameObject.FindGameObjectWithTag("Targeter").GetComponent<target_selector>();
        treasures = new List<string>();

        inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");

        for(int i = 0; i < inventoryItemList.itemList.Count; i++)
        {
            treasures.Add(inventoryItemList.itemList[i].itemName);
        }

        treasures[0] = "Back";

        for (int i = 0; i < treasures.Count; i++)
		{
			GameObject temp_button = Instantiate(button);
			temp_button.transform.SetParent(gameObject.transform, false);
			temp_button.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * 30, 30);
			temp_button.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
			temp_button.GetComponentInChildren<Text>().text = treasures[i];
		}
	}

	public void ButtonPressed(GameObject button)
	{
		// Add in logic here to equip object that came out of chest
		// The below line sets the frame marker. It is commented out because GameControl needs a new field in model_player called treasure (or whatever) but since Sam's editing it I can't change it right now.

		//GameControl.control.state.frame_markers[targetSel.target.GetComponentInParent<FrameMarkerController>().frame_marker_identifier].treasure = treasures[button.GetComponentInChildren<Text>().text];

		// playerMaker.cs has an example for creating the players so if this makes no sense blame Sam because he made that

		objectSelector.SetActive(true);
		gameObject.transform.parent.gameObject.SetActive(false);
	}
}