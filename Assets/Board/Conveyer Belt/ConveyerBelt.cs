using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Bots;
using System;

//TODO Make this a scriptableobject
namespace Robo.Board{
	[SelectionBase]
	public class ConveyerBelt : Waypoint, IObstacle {
		[SerializeField] Vector2Int direction = new Vector2Int(0,1);
		
		Vector2Int movePosition;

		public string GetObstacleName(){
			return "Conveyer Belt";
		}

		public void endTurnTrigger(BotMovement bot){
			MoveBot(bot);
		}

		//TODO Bot will not finish movement before next turn begins
        private void MoveBot(BotMovement bot)
        {
            var moveDirectionPower = new Vector2Int(direction.x, direction.y);
            movePosition = new Vector2Int(
                Mathf.RoundToInt(bot.transform.position.x + moveDirectionPower.x),
                Mathf.RoundToInt(bot.transform.position.z + moveDirectionPower.y)
            );
            var destination = bot.SetDestinationWaypoint(movePosition);
			StartCoroutine(bot.HandleMovement(false));
        }
    }
}