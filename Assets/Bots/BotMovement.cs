using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Board;
using Robo.Cards;
using System;

namespace Robo.Bots
{
	//TODO Figure out how to reduce code here
    [RequireComponent(typeof(GridPositionHandler))]
    public class BotMovement : MonoBehaviour
    {
        [SerializeField] Vector2Int currentDirection;
        [SerializeField] float moveSpeed = .5f;
        [SerializeField] CardConfig spamCard;

        GridPositionHandler gridPositionHandler;
        TurnManager turnManager;
        Queue<Command> commandQueue = new Queue<Command>();
        Command currentCommand;
        
        float desiredRotation;
        Quaternion finalRotation;

        public bool isAlive = true;
        bool actionSubmitted = false;
        int cardIndex = 0;
        public List<CardConfig> cards; //TODO make getter/setter


        void Start()
        {
            gridPositionHandler = GetComponent<GridPositionHandler>();
            turnManager = FindObjectOfType<TurnManager>();
            cards = new List<CardConfig>();
            turnManager.onActivateObstacles += OnActivateObstacles;
            turnManager.onActivateCollectibles += OnActivateCollectibles;
            turnManager.onFireLasers += OnFireLasers;
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
            if (distanceBetweenWaypoints > gridPositionHandler.waypointThreshold)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, step);
                distanceBetweenWaypoints = (transform.position - destination.transform.position).magnitude;
                CheckForBotCollision(destination, direction);
            }else{
                gridPositionHandler.FixPositionToWaypoint();
                GetNewCommandInQueue();
            }
        }

        private void CheckForBotCollision(Waypoint destination, Vector3 direction)
        {
            List<BotMovement> bots = new List<BotMovement>(FindObjectsOfType<BotMovement>());
            bots.Remove(this);
            foreach (BotMovement bot in bots){
                Waypoint botWaypoint = bot.GetComponent<GridPositionHandler>().CurrentWaypoint;
                if (botWaypoint == destination){
                    PushBot(bot, direction);
                }
            }
        }

        public void PushBot(BotMovement bot, Vector3 direction)
        {
            bot.transform.position += direction;
            Waypoint result = bot.gridPositionHandler.FixPositionToWaypoint();
            if (result == null) { bot.DestroyBot(); }
            if (bot.isAlive){
                bot.CheckForBotCollision(bot.GetComponent<GridPositionHandler>().CurrentWaypoint, direction);
            }
        }



        //TODO Change to respawn
        public void DestroyBot()
        {
            print("KILLING BOT");
            isAlive = false;
            turnManager.onActivateObstacles -= OnActivateObstacles;            
            turnManager.onActivateCollectibles -= OnActivateCollectibles;
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

        void OnActivateCollectibles(){
            CollectibleHandler[] collectibles = gridPositionHandler.CurrentWaypoint.GetComponentsInChildren<CollectibleHandler>();
            foreach (CollectibleHandler collectible in collectibles){
                collectible.Activate(gameObject);
            }
        }

        void OnActivateObstacles(){            
            if (gridPositionHandler.CurrentWaypoint.GetComponent<IObstacle>() != null){
                gridPositionHandler.CurrentWaypoint.GetComponent<IObstacle>().endTurnTrigger(this);
            }else{
                turnManager.AddPlayerToQueue(this);
            }
        }

        //TODO Add laser particle
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