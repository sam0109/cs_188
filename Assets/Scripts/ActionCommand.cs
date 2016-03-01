using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionCommand : MonoBehaviour
{
    public Button btn;

	void Start ()
    {
	    btn.onClick.AddListener(delegate { onAction(); });
    }

    public void onAction()
    {
        PlayerAttack pa = new PlayerAttack();
        pa.Attack();
    }
}
