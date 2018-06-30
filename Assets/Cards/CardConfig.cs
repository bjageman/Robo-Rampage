using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Robo.Bots;

namespace Robo.Cards{
    public abstract class CardConfig : ScriptableObject
    {

        [SerializeField] string cardName;
        [SerializeField] int cardCost;
        [SerializeField] Sprite cardSprite;
        [SerializeField] bool destroyCardAfterPlaying = false;

        public string CardName { get { return cardName; } }
        public int CardCost { get { return cardCost; } }
        public Sprite CardSprite { get { return cardSprite; } }
        public bool DestroyCardAfterPlaying { get { return destroyCardAfterPlaying; } }

        protected CardBehavior behavior;

        public abstract CardBehavior GetBehaviorComponent(GameObject objectToAttachTo);

        public CardBehavior AttachAbilityTo(GameObject objectToAttachTo)
        {
            CardBehavior behaviorComponent = GetBehaviorComponent(objectToAttachTo);
            behaviorComponent.Config = this;
            behavior = behaviorComponent;
            return behavior;
        }

        public void Use(BotMovement bot)
        {
            behavior.Use(bot);
        }
    }
}