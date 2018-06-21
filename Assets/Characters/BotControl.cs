using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class BotControl : MonoBehaviour {

	[SerializeField] Vector2Int currentDirection;
	[SerializeField] float dwellTime = 1f;
	[SerializeField] float moveSpeed = .5f;
	
	[SerializeField] Register register;
	[SerializeField][Range(.01f,.5f)] float waypointThreshold = .1f;

	Waypoint currentWaypoint; 
	Waypoint destinationWaypoint;
	BoardProcessor board;
	CardCommands cardCommands;
	TurnHandler turnHandler;

	Rigidbody rigidBody;
	bool isMoving = false;
	int playerTurn = 0;
	int cardIndex = 0;
	CardConfig[] cards;

	public void SetDestinationWaypoint(Vector2Int waypoint){
		destinationWaypoint = board.GetNearestWaypoint(waypoint);
	}

	public void SetDestinationWaypoint(int x, int y){
		SetDestinationWaypoint(new Vector2Int(x, y));
	}

	void Awake(){
		turnHandler = FindObjectOfType<TurnHandler>();
		SetupInitialBoardPosition ();
	}

	void Start(){
		cardCommands = GetComponent<CardCommands>();
		cards = cardCommands.getCards();
		cardCommands.RunRegister();
	}

	void Update(){
		if (playerTurn == turnHandler.currentTurnNumber && cards.Length > cardIndex)
        {
			cards[cardIndex].Use(null);
            StartCoroutine(HandleMovement());
			turnHandler.currentTurnNumber++;
			cardIndex++;
        }
    }

    private IEnumerator HandleMovement()
    {
		float distanceBetweenWaypoints = (transform.position - destinationWaypoint.transform.position).magnitude;
        while (distanceBetweenWaypoints > waypointThreshold)
        {
            isMoving = true;
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destinationWaypoint.transform.position, step);
			distanceBetweenWaypoints = (transform.position - destinationWaypoint.transform.position).magnitude;
			yield return new WaitForEndOfFrame();
		}
		playerTurn++;
        FixPositionToWaypoint();
    }

    void SetupInitialBoardPosition ()
	{
		board = FindObjectOfType<BoardProcessor> ();
		currentWaypoint = board.GetNearestWaypoint (transform.position.x, transform.position.z, waypointThreshold);
		transform.position = currentWaypoint.transform.position;
		destinationWaypoint = currentWaypoint;
	}

	public void MoveToWaypoint(Vector2Int position){
		MoveToWaypoint(position.x, position.y);
	}

	public void MoveToWaypoint(int x, int y){
		print("Moving to " + x + "," + y);
		var waypoint = board.GetNearestWaypoint(x,y);
		SetDestinationWaypoint(x, y);		
	}

    private void FixPositionToWaypoint()
    {
        var nearestWaypoint = board.GetNearestWaypoint(transform.position.x, transform.position.z, waypointThreshold);
        transform.position = nearestWaypoint.transform.position;
    }

    void RotateBot (int numRotations)
	{
		int zRotation = Mathf.RoundToInt (transform.rotation.z)  + (90 * numRotations);
		//todo Set direction facing with rotation
		transform.Rotate (
			0f, 
			0f, 
			zRotation
		);
	}

	public Vector2Int GetFacingDirection(){
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
}
