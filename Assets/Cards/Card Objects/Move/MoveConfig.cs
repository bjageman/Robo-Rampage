using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robo.Cards{
	[CreateAssetMenu(menuName = "Cards/Move")]
	public class MoveConfig : CardConfig {

		[Header("Move Config")]
		[SerializeField] int moveSpaces = 1;
		[SerializeField] bool reverseMovement = false;

		public int MoveSpaces { get { return moveSpaces; }}
		public bool ReverseMovement { get { return reverseMovement; }}

		public override CardBehavior GetBehaviorComponent(GameObject objectToAttachTo){
			return objectToAttachTo.AddComponent<MoveBehavior>();
		}
	}
}