using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class character_controller : MonoBehaviour {
    public float range;
	target_selector target_selector;
	public attack_values values;
	public List<GameObject> models;
	GameObject current_model;
	int current_model_num;
	string myPlayer;

    RaycastHit hit;
    // Use this for initialization
    void Start () {
		current_model_num = -1;
        target_selector = GameObject.FindGameObjectWithTag("Targeter").GetComponent<target_selector>();
		myPlayer = gameObject.transform.parent.GetComponent<FrameMarkerController> ().owner;
    }
	
	// Update is called once per frame
	void Update () {
		if (GameControl.control.state != null &&
			GameControl.control.state.Characters != null &&
			GameControl.control.state.Characters.ContainsKey(myPlayer))
		{
			if (current_model_num != GameControl.control.state.Characters[gameObject.transform.parent.GetComponent<FrameMarkerController>().owner].model)
			{
				UpdateModel(GameControl.control.state.Characters[gameObject.transform.parent.GetComponent<FrameMarkerController>().owner].model);
			}
		}
		else
		{
			if (current_model_num != GameControl.control.playerCharacter)
			{
				UpdateModel(GameControl.control.playerCharacter);
			}
		}
	}

	public int randomNum (int max)
	{
		//Random rnd = new Random ();
		//int answ = rnd.Next (1, 21);
		//return answ;
		int answ = Random.Range(1, max);
		return answ;
	}

    public void Attack ()
    {
		string playerID = GameControl.control.match.SelfParticipantId;

		if (GameControl.control.isMyTurn && myPlayer == playerID) 
		{

			int diceRollHitOrNot = randomNum (20);
		
			Character chara = new Character();
			chara = GameControl.control.state.Characters [playerID];
			int strength = chara.str;

			stat_converter mod = new stat_converter (strength);
			int strengthMod = mod.modifierValue;

			int diceRollDamage = randomNum (8);
			int attackDamage = strengthMod + diceRollDamage;

			values = new attack_values (diceRollHitOrNot, attackDamage);

			if (target_selector.target)
			{
				if ((target_selector.target.transform.position - transform.position).magnitude > range)
				{
					print("Too far away!");
				}
				else
				{
					target_selector.target.BroadcastMessage("Damage", values);
				}
			}
			else
			{
				print("Nothing selected!");
			}

            if (gameObject.GetComponent<Animation>()["Attack"])
            {
                gameObject.GetComponent<Animation>().Play("Attack");
            }
        }
    }

	void UpdateModel(int new_model)
	{
		if(current_model)
		{
			Destroy(current_model);
		}
		current_model = Instantiate(models[new_model]);
		current_model_num = new_model;
		current_model.transform.SetParent(gameObject.transform, false);
	}
}

public class attack_values
{
	public attack_values(int diceRollVal, int attackDamageVal)
	{
		diceRollToHit = diceRollVal;
		attackDamageWithDice = attackDamageVal;
	}

	public int diceRollToHit;
	public int attackDamageWithDice;
}

public class stat_converter
{
	public stat_converter(int stat)
	{
		modifierValue = (stat / 2) - 5;
	}

	public int modifierValue;
}
