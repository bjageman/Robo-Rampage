using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robo.Commands{
    [CreateAssetMenu(menuName = "Cards/Spam")]
    public class SpamConfig : CardConfig
    {

        //TODO Attach to bot that's calling it
        public override CardBehavior GetBehaviorComponent(GameObject objectToAttachTo)
        {
            return objectToAttachTo.AddComponent<SpamBehavior>();
        }
    }
}