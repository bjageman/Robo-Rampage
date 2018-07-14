using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CollectibleBehavior : MonoBehaviour
{
	protected CollectibleConfig config;

	public CollectibleConfig Config { set { this.config = value; } }

	public abstract void Use(GameObject collector);
}

