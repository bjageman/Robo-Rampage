using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Bots;

namespace Robo.Commands{

    public abstract class CardBehavior : MonoBehaviour
    {
        protected CardConfig config;

        public CardConfig Config { set { this.config = value; } }

        public abstract void Use(BotMovement bot);
    }
}
