using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Bots;
using Robo.Board;

namespace Robo.Cards{
    public class MoveBehavior : CardBehavior {

        Vector2Int movePosition;
        BoardProcessor board;
        
        //TODO Consider more elegant solution to reverse movement
        public override void Use(BotMovement bot)
        {
            board = FindObjectOfType<BoardProcessor>();
            Vector3 previousPosition = bot.transform.position;
            for (int i = 0; i < (config as MoveConfig).MoveSpaces; i++){
                Waypoint nextWaypoint;
                if ((config as MoveConfig).ReverseMovement){
                    nextWaypoint = GetBackwardDirectionWaypoint(bot, previousPosition);
                }else{
                    nextWaypoint = GetForwardDirectionWaypoint(bot, previousPosition);
                }
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

        private Waypoint GetBackwardDirectionWaypoint(BotMovement bot, Vector3 nextPosition)
        {   
            return board.GetNearestWaypoint(new Vector2Int(
                Mathf.RoundToInt(nextPosition.x - bot.transform.forward.x),
                Mathf.RoundToInt(nextPosition.z - bot.transform.forward.z)
            ));
        }

        private Waypoint GetForwardDirectionWaypoint(BotMovement bot, Vector3 nextPosition)
        {   
            return board.GetNearestWaypoint(new Vector2Int(
                Mathf.RoundToInt(nextPosition.x + bot.transform.forward.x),
                Mathf.RoundToInt(nextPosition.z + bot.transform.forward.z)
            ));
        }
    }
}