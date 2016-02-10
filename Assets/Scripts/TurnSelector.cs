﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class TurnSelector : MonoBehaviour {
    public GameObject button;
    target_selector targetSel;
    List<GameObject> buttons;
    Dictionary<string, string> players;

    void Start()
    {
        targetSel = GameObject.FindGameObjectWithTag("Targeter").GetComponent<target_selector>();
        players = GameControl.control.GetPlayers();
        int i = 0;
        foreach (KeyValuePair<string,string> kvp in players)
        {
            GameObject temp_button = Instantiate(button);
            temp_button.transform.SetParent(gameObject.transform, false);
            temp_button.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * 30, 30);
            temp_button.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            temp_button.GetComponentInChildren<Text>().text = kvp.Key;
            temp_button.GetComponent<ButtonIdentifier>().buttonID = i;
            i++;
        }
    }

    public void ButtonPressed(GameObject pressed_button)
    {
        GameControl.control.TakeTurn(players[pressed_button.GetComponentInChildren<Text>().text]);
    }
}
