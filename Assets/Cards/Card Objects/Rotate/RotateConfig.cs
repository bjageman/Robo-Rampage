using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Rotate")]
public class RotateConfig : CardConfig {

	[Header("Rotate Config")]
	[SerializeField] int numRotations = 1;

	public int NumRotations { get { return numRotations; }}

	public override CardBehavior GetBehaviorComponent(GameObject objectToAttachTo){
		return objectToAttachTo.AddComponent<RotateBehavior>();
	}
}
