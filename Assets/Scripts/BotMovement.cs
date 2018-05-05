using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class BotMovement : MonoBehaviour {

	[SerializeField] Vector2Int currentDirection;
	[SerializeField] float dwellTime = 1f;

	List<Command> discardPile;
	Dictionary<Vector2Int, Waypoint> grid;
	Waypoint currentWaypoint; 
	BoardProcessor board;

	void Start(){
		Setup ();
		ProcessMoves ();
	}

	void Setup ()
	{
		board = GetComponentInParent<BoardProcessor> ();
		currentWaypoint = board.GetWaypoint (
			new Vector2Int (
				Mathf.RoundToInt(transform.position.x), 
				Mathf.RoundToInt(transform.position.y))
		);
		print ("Current waypoint " + currentWaypoint.GetGridPosition());
	}

	void ProcessMoves ()
	{
		PlayerCards playerCards = GetComponent<PlayerCards> ();
		playerCards.MockHand ();
		var hand = playerCards.GetHand ();
		StartCoroutine (FollowCommands (hand));
	}

	IEnumerator FollowCommands(List<Command> commands){
		foreach (Command command in commands) {
			//print (command.GetTitle() + " " + command.GetPower ());
			if (command.GetTitle() == "Move") {
				for (int x = 0; x < command.GetPower (); x++) {
					var direction = DetermineMoveDirection ();
					MoveBotForward (direction);
				}
			} else if (command.GetTitle() == "Rotate") {
				ProcessRotation (command);
			} else {
				print ("Unknown command");
			}
			yield return new WaitForSeconds (dwellTime);
		}
	}

	void MoveBotForward(Vector2Int direction){
		//var newPosition = transform.position + direction;
		Waypoint newWaypoint = board.GetWaypoint(currentWaypoint.GetGridPosition() + direction);
		if (newWaypoint != null) {
			transform.position = newWaypoint.transform.position;
			currentWaypoint = newWaypoint;
		}
	}

	Vector2Int DetermineMoveDirection(){
		float eulerAngle = transform.eulerAngles.y;
		int rotationDegrees = Mathf.RoundToInt (eulerAngle / 90) * 90;
		float rotationRadians = (rotationDegrees * Mathf.PI) / 180;
		int upDownDirectionRaw = Mathf.RoundToInt(Mathf.Sin(rotationRadians));
		int leftRightDirectionRaw = Mathf.RoundToInt(Mathf.Cos(rotationRadians));
		if (upDownDirectionRaw == 0) {
			if (leftRightDirectionRaw == 1) {
				return Vector2Int.left;
			} else {
				return Vector2Int.right;
			}
		} else if (upDownDirectionRaw == 1) {
			return Vector2Int.up;
		} else if (upDownDirectionRaw == -1) {
			return Vector2Int.down;
		}
		Debug.LogWarning ("invalid direction");
		return new Vector2Int (0, 0);
	}

	void ProcessRotation (Command command)
	{
		int zRotation = Mathf.RoundToInt (transform.rotation.z)  + (90 * command.GetPower ());
		//todo Set direction facing with rotation
		transform.Rotate (
			0f, 
			0f, 
			zRotation
		);
	}
}
