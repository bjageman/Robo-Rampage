using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Collectibles/Energy Orb")]
public class EnergyOrbCollectible : CollectibleConfig {
	[Header("Energy")]
	[SerializeField] int energyAmount = 1;

	public int EnergyAmount { get { return energyAmount; }}

	public override CollectibleBehavior GetBehaviorComponent(GameObject objectToAttachTo){
		return objectToAttachTo.AddComponent<EnergyOrbBehavior>();
	}
	
}
