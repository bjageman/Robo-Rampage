using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Move")]
public class MoveConfig : CardConfig {

	[Header("Move Config")]
	[SerializeField] int moveSpaces = 1;

	public int MoveSpaces { get { return moveSpaces; }}

	public override CardBehavior GetBehaviorComponent(GameObject objectToAttachTo){
		return new MoveBehavior();
	}
}
