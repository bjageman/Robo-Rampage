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
		[SerializeField] float moveSpeed = 3f;
		Vector3 movePosition;
		BotMovement bot;

		bool isMoving = false;

		public string GetObstacleName(){
			return "Conveyer Belt";
		}

		public void endTurnTrigger(BotMovement bot){
			MoveBot(bot);
		}

		public void Update(){
			if (isMoving){
				HandleMovement();
			}
		}

		public void HandleMovement(bool submitTurn = true) //TODO Handle this parameter better
        {
            Transform botTransform = bot.transform;
			float distanceBetweenWaypoints = (botTransform.position - movePosition).magnitude;
			
            if (distanceBetweenWaypoints > bot.waypointThreshold)
            {
                float step = moveSpeed * Time.deltaTime;
                botTransform.position = Vector3.MoveTowards(botTransform.position, movePosition, step);
                distanceBetweenWaypoints = (transform.position - movePosition).magnitude;
            }else{
                print("Obstacle completed");
				isMoving = false;
            }
        }

		//TODO Bot will not finish movement before next turn begins
		//Perhaps move Handlemovement to here and put it in an Update
        private void MoveBot(BotMovement bot)
        {
			this.bot = bot;
            var moveDirectionPower = new Vector2Int(direction.x, direction.y);
            movePosition = new Vector3(
                Mathf.RoundToInt(bot.transform.position.x + moveDirectionPower.x),
                Mathf.RoundToInt(bot.transform.position.z + moveDirectionPower.y),
				0
            );
			isMoving = true;
        }
    }
}