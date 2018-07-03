using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Bots;
using Robo.Board;

namespace Robo.Cards{
    public class MoveBehavior : CardBehavior {

        Vector2Int movePosition;
        BoardProcessor board;
        
        //TODO When the destination doesn't exist, reduce the movement by 1 until it does
        public override void Use(BotMovement bot)
        {
            board = FindObjectOfType<BoardProcessor>();
            Vector3 previousPosition = bot.transform.position;
            for (int i = 0; i < (config as MoveConfig).MoveSpaces; i++){
                Waypoint nextWaypoint = GetForwardDirectionWaypoint(bot, previousPosition);
                if (nextWaypoint != null){
                     MoveBot(bot, nextWaypoint);
                     previousPosition = nextWaypoint.transform.position;
                }    
            }
            Destroy(bot.GetComponent<MoveBehavior>());
        }

        private void MoveBot(BotMovement bot, Waypoint nextWaypoint)
        {
            bot.AddCommandToQueue(new Command("MOVE", nextWaypoint));
        }

        private Waypoint GetForwardDirectionWaypoint(BotMovement bot, Vector3 nextPosition)
        {   
            print(bot.transform.forward);
            return board.GetNearestWaypoint(new Vector2Int(
                Mathf.RoundToInt(nextPosition.x + bot.transform.forward.x),
                Mathf.RoundToInt(nextPosition.z + bot.transform.forward.z)
            ));
        }
    }
}