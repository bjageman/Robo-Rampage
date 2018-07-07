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
        [SerializeField] CardConfig spamCard;

        Waypoint currentWaypoint;
        BoardProcessor board;
        TurnManager turnManager;
        Queue<Command> commandQueue = new Queue<Command>();
        Command currentCommand;
        
        float desiredRotation;
        Quaternion finalRotation;

        public bool isAlive = true;
        bool actionSubmitted = false;
        int cardIndex = 0;
        public List<CardConfig> cards; //TODO make getter/setter

        public Waypoint CurrentWaypoint { 
            get { return currentWaypoint; }
            set {currentWaypoint = value; } }

        void Start()
        {
            turnManager = FindObjectOfType<TurnManager>();
            SetupInitialBoardPosition();
            cards = new List<CardConfig>();
            turnManager.onActivateObstacles += OnActivateObstacles;
            turnManager.onFireLasers += OnFireLasers;
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
        //TODO Handle blocked movement (note: walls need to be implemented)
        public void MoveToWaypoint(Waypoint destination) 
        {
            if (destination == null ){ return; }
            float distanceBetweenWaypoints = (transform.position - destination.transform.position).magnitude;
            Vector3 direction = (destination.transform.position - transform.position);
            if (distanceBetweenWaypoints > waypointThreshold)
            {
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
            if (bot.isAlive){
                bot.CheckForBotCollision(bot.currentWaypoint, direction);
            }
        }

        public void FixPositionToWaypoint()
        {
            var nearestWaypoint = board.GetNearestWaypoint(transform.position.x, transform.position.z, waypointThreshold);
            if (nearestWaypoint == null){
                DestroyBot();
            }else{
                transform.position = nearestWaypoint.transform.position;
                currentWaypoint = nearestWaypoint;
            }            
            
        }

        //TODO Change to respawn
        private void DestroyBot()
        {
            isAlive = false;
            turnManager.onActivateObstacles -= OnActivateObstacles;
            turnManager.onFireLasers -= OnFireLasers;
            turnManager.RemovePlayerFromQueue(this);
            Destroy(this.gameObject);
            //Respawn() //TODO implement later
        }

        // TODO Animate rotations
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
                    RotateBot(currentCommand.amount); 
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

        private void OnFireLasers()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                BotMovement bot = hit.collider.GetComponent<BotMovement>();
                if (bot != null){
                    Deck deck = bot.GetComponent<Deck>();
			        deck.DiscardCard(spamCard);
                }
                
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