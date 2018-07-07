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
        [SerializeField] [Range(.01f, .5f)] public float waypointThreshold = .1f; 

        Waypoint currentWaypoint;
        BoardProcessor board;
        TurnManager turnManager;
        Queue<Command> commandQueue = new Queue<Command>();
        Command currentCommand;
        
        float desiredRotation;
        Quaternion finalRotation;

        bool actionSubmitted = false;
        bool isMoving = false;
        int cardIndex = 0;
        public List<CardConfig> cards; //TODO make getter/setter

        public bool IsMoving { 
            get { return isMoving; }
            set { isMoving = value; }    
        }
        public Waypoint CurrentWaypoint { 
            get { return currentWaypoint; }
            set {currentWaypoint = value; } }

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
        }

        //TODO Consider moving to BotMovement
        void Update()
        {
            if (this == turnManager.getActiveTurn())
            {
                HandleActions();
            }
        }
        
        public void AddCommandToQueue(Command command){
            commandQueue.Enqueue(command);
        }

        public void ProcessNextRound()
        {
            cardIndex = 0;
            cards.Clear();
        }

        public void PlayCard(int cardIndex){
            if (cardIndex < cards.Count){
                cards[cardIndex].AttachAbilityTo(gameObject);
			    cards[cardIndex].Use(this);
            }
		}

        //TODO Going over the board should lead to death
        //TODO Handle blocked movement
        public void MoveToWaypoint(Waypoint destination) 
        {
            if (destination == null ){ return; }
            float distanceBetweenWaypoints = (transform.position - destination.transform.position).magnitude;
            Vector3 direction = (destination.transform.position - transform.position);
            if (distanceBetweenWaypoints > waypointThreshold)
            {
                isMoving = true;
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, step);
                distanceBetweenWaypoints = (transform.position - destination.transform.position).magnitude;
                CheckForBotCollision(destination, direction);
            }else{
                FixPositionToWaypoint();
                GetNewCommandInQueue();
            }
        }

        private void CheckForBotCollision(Waypoint destination, Vector3 direction)
        {
            List<BotMovement> bots = new List<BotMovement>(FindObjectsOfType<BotMovement>());
            bots.Remove(this);
            foreach (BotMovement bot in bots){
                Waypoint botWaypoint = bot.currentWaypoint.GetComponent<Waypoint>();
                if (botWaypoint == destination){
                    PushBot(bot, direction);
                }
            }

        }

        public void PushBot(BotMovement bot, Vector3 direction)
        {
            bot.transform.position += direction;
            bot.FixPositionToWaypoint();
            bot.CheckForBotCollision(bot.currentWaypoint, direction);
        }

        public void FixPositionToWaypoint()
        {
            var nearestWaypoint = board.GetNearestWaypoint(transform.position.x, transform.position.z, waypointThreshold);
            transform.position = nearestWaypoint.transform.position;
            currentWaypoint = nearestWaypoint;
            isMoving = false;
        }

        // TODO Animate rotations
        // TODO Rotation is bugged
        public void RotateBot(int numRotations)
        {
            int yRotation = 90 * numRotations;
            transform.Rotate(
                0f,
                yRotation,
                0
            );

            GetNewCommandInQueue();
        }

        public void AddCardToProcessor(CardConfig card) { cards.Add(card); }

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

        void OnActivateObstacles(){
            //TODO Make this look up any obstactle attached to a waypoint
            if (currentWaypoint.GetComponent<IObstacle>() != null){
                currentWaypoint.GetComponent<IObstacle>().endTurnTrigger(this);
            }else{
                turnManager.AddPlayerToQueue(this);
            }
        }

        //TODO Double collision breaks
        //TODO Make it work with null
        //TODO Not fluid movement
        // void OnTriggerEnter(Collider other) {
        //     BotMovement bot = other.GetComponent<BotMovement>();
        //     if (isMoving && bot != null)
        //     {
        //         bot.IsMoving = true;
        //         //TODO Movement can be made better here
        //         PushBot(bot);
        //     }
        // }

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