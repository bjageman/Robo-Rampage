using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Board;
using Robo.Cards;
using System;

namespace Robo.Bots
{
	//TODO Figure out how to reduce code here
    public class BotMovement : MonoBehaviour
    {
        [SerializeField] Vector2Int currentDirection;
        [SerializeField] float moveSpeed = .5f;
        [SerializeField] [Range(.01f, .5f)] public float waypointThreshold = .1f; //TODO Make Getter

        Waypoint currentWaypoint;
        Waypoint destinationWaypoint;
        BoardProcessor board;
        TurnManager turnManager;
        Queue<Command> commandQueue = new Queue<Command>();
        Command currentCommand;
        
        bool rotationStarted = false;
        float desiredRotation;
        Quaternion finalRotation;

        bool actionSubmitted = false;
        int cardIndex = 0;
        public List<CardConfig> cards; //TODO make getter/setter

        public void SetCurrentWaypoint(Waypoint waypoint) { currentWaypoint = waypoint; }

        public Waypoint SetDestinationWaypoint(int x, int y) { return SetDestinationWaypoint(new Vector2Int(x, y)); }
        public Waypoint SetDestinationWaypoint(Vector2Int waypoint)
        {
            destinationWaypoint = board.GetNearestWaypoint(waypoint);
            return destinationWaypoint;
        }

        void Start()
        {
            turnManager = FindObjectOfType<TurnManager>();
            SetupInitialBoardPosition();
            cards = new List<CardConfig>();
            turnManager.onActivateObstacles += OnActivateObstacles;
        }

        void SetupInitialBoardPosition()
        {
            board = FindObjectOfType<BoardProcessor>();
            currentWaypoint = board.GetNearestWaypoint(transform.position.x, transform.position.z, waypointThreshold);
            transform.position = currentWaypoint.transform.position;
            destinationWaypoint = currentWaypoint;
        }

        //TODO Consider moving to BotMovement
        void Update()
        {
            bool isCurrentBotsTurn = (this == turnManager.getActiveTurn()); //Bot is in the current queue (prevents simulatenous play)
            if (isCurrentBotsTurn)
            {
                HandleActions();
            }
        }

        private void HandleActions()
        {
            if (!actionSubmitted){
                PlayCard(cardIndex);
                actionSubmitted = true;
            }
            if (currentCommand.action == null){
                GetNewCommandInQueue();
            }else{
                if (currentCommand.action == "MOVE"){
                    MoveToWaypoint(currentCommand.waypoint);
                }else if (currentCommand.action == "ROTATE"){
                    RotateBot(currentCommand.amount); //TODO set to var
                }
            }
            
        }

        public void AddCommandToQueue(Command command){
            commandQueue.Enqueue(command);
        }

        private void GetNewCommandInQueue()
        {
            currentCommand.action = null;
            if (commandQueue.Count > 0){
                currentCommand = commandQueue.Dequeue();
            }
            else
            {
                if (turnManager.ObstaclesActivated){
                    TurnCompleted();
                }
                turnManager.submitTurn(this);
                //TODO Will be changed to an observer
                
            }
        }

        private void TurnCompleted()
        {
            currentCommand.action = null;
            actionSubmitted = false;
            cardIndex++;
        }

        public void ProcessNextRound()
        {
            cardIndex = 0;
            cards.Clear();
        }


        public void FinishAction(){
            actionSubmitted = false;
        }

        public void PlayCard(int cardIndex){
            if (cardIndex < cards.Count){
                cards[cardIndex].AttachAbilityTo(gameObject);
			    cards[cardIndex].Use(this);
            }
		}

        //TODO Going over the board should lead to death
        public void MoveToWaypoint(Waypoint destination) //TODO Handle this parameter better
        {
            float distanceBetweenWaypoints = (transform.position - destination.transform.position).magnitude;
            if (distanceBetweenWaypoints > waypointThreshold)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, step);
                distanceBetweenWaypoints = (transform.position - destination.transform.position).magnitude;
            }else{
                FixPositionToWaypoint();
                GetNewCommandInQueue();
            }
        }

        public void FixPositionToWaypoint()
        {
            var nearestWaypoint = board.GetNearestWaypoint(transform.position.x, transform.position.z, waypointThreshold);
            transform.position = nearestWaypoint.transform.position;
            currentWaypoint = nearestWaypoint;
        }

        // TODO Animate rotations
        // TODO Rotation is bugged
        public void RotateBot(int numRotations)
        {
            int zRotation = 90 * numRotations;
            transform.Rotate(
                0f,
                0f,
                zRotation
            );

            GetNewCommandInQueue();
        }

        // public void RotateBot(int numRotations)
        // {
        //     int rotationInDegrees = 90 * numRotations;
        //     if (rotationStarted){
        //         finalRotation = Quaternion.Euler( 0, rotationInDegrees, 0 ) * transform.rotation;
        //         print(Mathf.Abs(transform.rotation.eulerAngles.y - desiredRotation));
        //         if (transform.rotation != finalRotation){
        //             transform.rotation = Quaternion.Lerp(transform.rotation, finalRotation, Time.deltaTime * moveSpeed);
        //         }else{
        //             rotationStarted = false;
        //             GetNewCommandInQueue();
        //         }
        //     }else{
        //         rotationStarted = true;
        //         desiredRotation = transform.rotation.eulerAngles.y + rotationInDegrees;
        //         print(rotationInDegrees + "  / " + desiredRotation);
        //     }
        // }

        //TODO Try replacing with .forward direction
        public Vector2Int GetFacingDirection()
        {
            float eulerAngle = transform.eulerAngles.y;
            int rotationDegrees = Mathf.RoundToInt(eulerAngle / 90) * 90;
            float rotationRadians = (rotationDegrees * Mathf.PI) / 180;
            int upDownDirectionRaw = Mathf.RoundToInt(Mathf.Sin(rotationRadians));
            int leftRightDirectionRaw = Mathf.RoundToInt(Mathf.Cos(rotationRadians));
            if (upDownDirectionRaw == 0)
            {
                if (leftRightDirectionRaw == 1)
                {
                    return Vector2Int.left;
                }
                else
                {
                    return Vector2Int.right;
                }
            }
            else if (upDownDirectionRaw == 1)
            {
                return Vector2Int.up;
            }
            else if (upDownDirectionRaw == -1)
            {
                return Vector2Int.down;
            }
            Debug.LogWarning("invalid direction");
            return new Vector2Int(0, 0);
        }

        public void AddCardToProcessor(CardConfig card) { cards.Add(card); }

        void OnActivateObstacles(){
            //TODO Make this look up any obstactle attached to a waypoint
            if (currentWaypoint.GetComponent<IObstacle>() != null){
                currentWaypoint.GetComponent<IObstacle>().endTurnTrigger(this);
            }else{
                turnManager.AddPlayerToQueue(this);
            }
        }
    }
    public struct Command  
    {  
        public string action;  
        public Waypoint waypoint;  
        public int amount;
        public Command(string p1, Waypoint p2, int p3 = 0)
        {
            action = p1;
            waypoint = p2;
            amount = p3;
        }
    }  
}