using UnityEngine;
using System.Collections;

public class AddNew : MonoBehaviour {

	public GameObject Drop;
	public GameObject AddButton;
	public void OnButtonClick()
	{
		Drop.SetActive (true);
		AddButton.SetActive (false);

	}
}
