using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Bots;
using Robo.Board;

//TODO Make this a scriptableobject
namespace Robo.Board{
	[SelectionBase]
	public class ConveyerBelt : Waypoint, IObstacle {
		[SerializeField] Vector2Int direction = new Vector2Int(0,1);

		Vector3 movePosition;
		BotMovement bot;

		public string GetObstacleName(){
			return "Conveyer Belt";
		}

		public void endTurnTrigger(BotMovement bot){
			print("Conveyer activated");
			BoardProcessor board = FindObjectOfType<BoardProcessor>();
			TurnManager turnManager = FindObjectOfType<TurnManager>();
			Vector3 botPosition = bot.transform.position;
			Waypoint moveToWaypoint = board.GetNearestWaypoint(new Vector2Int(
                Mathf.RoundToInt(botPosition.x + direction.x),
                Mathf.RoundToInt(botPosition.z + direction.y)
            ));
			bot.AddCommandToQueue(new Command("MOVE", moveToWaypoint));
			turnManager.AddPlayerToQueue(bot);
		}
    }
}