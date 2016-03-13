using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DestroyMe : MonoBehaviour {
    public float timeToLive;
    float maxTimeToLive;

    void Start()
    {
        maxTimeToLive = timeToLive;
        gameObject.GetComponent<Text>().CrossFadeAlpha(0, maxTimeToLive, false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        timeToLive -= Time.deltaTime;
        if(timeToLive <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 oldPos = gameObject.GetComponent<RectTransform>().anchoredPosition;
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(oldPos.x, oldPos.y + ((timeToLive / maxTimeToLive) * Time.deltaTime * 30), oldPos.z);
        }
	}
}
