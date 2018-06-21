﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO Scriptable objects

public abstract class CardConfig : ScriptableObject {

	[SerializeField] string cardName;
	[SerializeField] int cardCost;
	[SerializeField] Image cardImage;

	protected CardBehavior behavior;

	public abstract CardBehavior GetBehaviorComponent(GameObject objectToAttachTo);

	public void AttachCardTo(GameObject objectToAttachTo){
		CardBehavior behaviorComponent = GetBehaviorComponent(objectToAttachTo);
		behaviorComponent.Config = this;
		behavior = behaviorComponent;
	}

	public void Use(GameObject target){
		behavior.Use(target);
	}

}