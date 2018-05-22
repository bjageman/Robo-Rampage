using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO Scriptable objects

[CreateAssetMenu(fileName = "New Card", menuName="Card")]
public class Card : ScriptableObject {

	public new string name;
	public string command;
	public int power;

	public Sprite image;

}
