using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Bots;

//TODO Make this a scriptableobject
namespace Robo.Board{
	public class ConveyerBelt : Waypoint, IObstacle {
		[SerializeField] int moveSpaces = 1;
		[SerializeField] Vector2Int direction = new Vector2Int(0,1);

		public void endTurnTrigger(BotMovement bot){
		}
	}
}