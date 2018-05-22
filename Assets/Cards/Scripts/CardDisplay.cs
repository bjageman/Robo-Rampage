using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {

	public Card card;

	public Text nameText;
	public string command;
	public int power;
	public Image image;

	// Use this for initialization
	void Start () {
		gameObject.name = card.name;
		nameText.text = card.name;
		command = card.command;
		power = card.power;
		image.sprite = card.image; 
	}
	
}
