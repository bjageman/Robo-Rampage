using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHandler : MonoBehaviour {
	[SerializeField] CollectibleConfig collectible;
	
	CollectibleBehavior behavior;
	List<GameObject> collectorsRecorded;

	void Start(){
		if (collectible != null){
			behavior = collectible.AttachAbilityTo(gameObject);
		}else{
			Debug.LogWarning("No collectible config attached to CollectibleHandler");
			Destroy(gameObject, .1f);
		}
	}
	
	public void Activate(GameObject collector){
		if (collectible.CollectOncePerBot){
			if (!collectorsRecorded.Contains(collector)){
				behavior.Use(collector);
				collectorsRecorded.Add(collector);
			}
		}else{
			behavior.Use(collector);
		}
		
		if (collectible.DestroyOnceCollected){
			Destroy(gameObject, .1f);
		}
	}
}
