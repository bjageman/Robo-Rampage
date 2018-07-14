using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Collectibles/Checkpoint Flag")]
public class CheckpointCollectible : CollectibleConfig {
	[Header("Checkpoint")]
	[SerializeField] int flagOrder = 1;

	public int FlagOrder { get { return flagOrder; }}

	public override CollectibleBehavior GetBehaviorComponent(GameObject objectToAttachTo){
		return objectToAttachTo.AddComponent<CheckpointBehavior>();
	}
	
}
