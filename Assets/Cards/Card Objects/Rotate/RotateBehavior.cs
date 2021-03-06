﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Bots;

namespace Robo.Cards{
	public class RotateBehavior : CardBehavior {

		Vector2Int direction;
		Vector2Int movePosition;

		public override void Use(BotMovement bot){
			bot.AddCommandToQueue(new Command("ROTATE", null, (config as RotateConfig).NumRotations));
			Destroy(bot.GetComponent<RotateBehavior>());
		}
		
	}
}