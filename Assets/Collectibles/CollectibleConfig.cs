using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Collectibles/_default")]
public abstract class CollectibleConfig : ScriptableObject {
	[SerializeField] string collectibleName;
	[SerializeField] bool destroyOnceCollected = true;
	[SerializeField] bool collectOncePerBot = false;
	[SerializeField] bool moveWithConveyerBelts = false;

	protected CollectibleBehavior behavior;

	public abstract CollectibleBehavior GetBehaviorComponent(GameObject objectToAttachTo);

	public string Name 					{ get { return collectibleName; }}
	public bool DestroyOnceCollected 	{ get { return destroyOnceCollected; }}
	public bool CollectOncePerBot 		{ get { return collectOncePerBot; }}
	public bool MoveWithConveyerBelts 	{ get { return moveWithConveyerBelts; }}

	public CollectibleBehavior AttachAbilityTo(GameObject objectToAttachTo)
        {
            CollectibleBehavior behaviorComponent = GetBehaviorComponent(objectToAttachTo);
            behaviorComponent.Config = this;
            behavior = behaviorComponent;
            return behavior;
        }
}
