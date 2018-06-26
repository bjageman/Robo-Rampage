using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Board;

public class BotMovement : MonoBehaviour {

	[SerializeField] Vector2Int currentDirection;
	[SerializeField] float moveSpeed = .5f;
	[SerializeField][Range(.01f,.5f)] float waypointThreshold = .1f;

	Waypoint currentWaypoint; 
	Waypoint destinationWaypoint;
	BoardProcessor board;
	TurnHandler turnHandler;

	void Start(){
		turnHandler = FindObjectOfType<TurnHandler>();
		SetupInitialBoardPosition ();
	}

	void SetupInitialBoardPosition ()
	{
		board = FindObjectOfType<BoardProcessor> ();
		currentWaypoint = board.GetNearestWaypoint (transform.position.x, transform.position.z, waypointThreshold);
		transform.position = currentWaypoint.transform.position;
		destinationWaypoint = currentWaypoint;
	}

	public void SetCurrentWaypoint(Waypoint waypoint){
		currentWaypoint = waypoint;
	}

	//TODO One of these seems redundant
	public Waypoint SetDestinationWaypoint(int x, int y){
		return SetDestinationWaypoint(new Vector2Int(x, y));
	}

	public Waypoint SetDestinationWaypoint(Vector2Int waypoint){
		destinationWaypoint = board.GetNearestWaypoint(waypoint);
		return destinationWaypoint;
	}

	public void MoveToWaypoint(Vector2Int position){
		MoveToWaypoint(position.x, position.y);
	}

	public void MoveToWaypoint(int x, int y){
		destinationWaypoint = SetDestinationWaypoint(x, y);		
	}


    //TODO Handle going over the board
    public IEnumerator HandleMovement()
    {
		float distanceBetweenWaypoints = (transform.position - destinationWaypoint.transform.position).magnitude;
        while (distanceBetweenWaypoints > waypointThreshold)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destinationWaypoint.transform.position, step);
			distanceBetweenWaypoints = (transform.position - destinationWaypoint.transform.position).magnitude;
			yield return new WaitForEndOfFrame();
		}
        FixPositionToWaypoint();
		turnHandler.submitTurn(this);
    }


    private void FixPositionToWaypoint()
    {
        var nearestWaypoint = board.GetNearestWaypoint(transform.position.x, transform.position.z, waypointThreshold);
        transform.position = nearestWaypoint.transform.position;
    }

	//TODO Animate rotations
    public void RotateBot (int numRotations)
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
