using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehavior : CardBehavior {

	Vector2Int direction;
	Vector2Int movePosition;
	
	//TODO When the destination doesn't exist, reduce the movement by 1 until it does
	public override void Use(BotControl bot)
    {
        direction = bot.GetFacingDirection();
        MoveBot(bot, (config as MoveConfig).MoveSpaces);
		Destroy(bot.GetComponent<MoveBehavior>());
    }

    private void MoveBot(BotControl bot, int moveSpaces)
    {
        var moveDirectionPower = new Vector2Int(direction.x * moveSpaces, direction.y * moveSpaces);
        movePosition = new Vector2Int(
            Mathf.RoundToInt(bot.transform.position.x + moveDirectionPower.x),
            Mathf.RoundToInt(bot.transform.position.z + moveDirectionPower.y)
        );
        var destination = bot.SetDestinationWaypoint(movePosition);
		if (destination == null){
			MoveBot(bot, moveSpaces -1);
		}
    }
}
