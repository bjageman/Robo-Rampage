using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour, IHasChanged {

	[SerializeField] Transform register;

	// Use this for initialization
	void Start () {
		HasChanged();
	}
	
	public void HasChanged(){
		//TODO Set up register card handler
		System.Text.StringBuilder builder = new System.Text.StringBuilder();
		builder.Append("Register: ");
		foreach (Transform card in register){
			Text item = card.GetComponent<Slot>().GetComponentInChildren<Text>();
			if (item){
				builder.Append(item.text);
				builder.Append (" - ");
			}
		}
	}
}

namespace UnityEngine.EventSystems{
	public interface IHasChanged: IEventSystemHandler{
		void HasChanged();
	}
}
