using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class BotMovement : MonoBehaviour {

	[SerializeField] Vector2Int currentDirection;
	[SerializeField] float dwellTime = 1f;
	
	[SerializeField] Register register;

	Dictionary<Vector2Int, Waypoint> grid;
	Waypoint currentWaypoint; 
	BoardProcessor board;

	void Start(){
		Setup ();
	}

	void Setup ()
	{
		board = GetComponentInParent<BoardProcessor> ();
		currentWaypoint = board.GetWaypoint (
			new Vector2Int (
				Mathf.RoundToInt(transform.position.x), 
				Mathf.RoundToInt(transform.position.y))
		);
	}

	public void ProcessMoves ()
	{
		PlayerCardsHandler playerCards = FindObjectOfType<PlayerCardsHandler> ();
		StartCoroutine (FollowCommands (register));
	}

	IEnumerator FollowCommands(Register register){
		var cards = register.GetComponentsInChildren<CardDisplay>();
		foreach (CardDisplay card in cards) {
			if (card == null){ break; }
			if (card.command == "Move") {
				for (int x = 0; x < card.power; x++) {
					var direction = DetermineMoveDirection ();
					MoveBotForward (direction);
				}
			} else if (card.command == "Rotate") {
				ProcessRotation (card.power);
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

	void ProcessRotation (int power)
	{
		int zRotation = Mathf.RoundToInt (transform.rotation.z)  + (90 * power);
		//todo Set direction facing with rotation
		transform.Rotate (
			0f, 
			0f, 
			zRotation
		);
	}
}
