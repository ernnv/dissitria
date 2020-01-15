using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    // Class variables
    private BattleStateMachine BSM;
    public BaseEnemy enemy;

    // StateMachine variables
    public enum TurnState
    {
        PROCESSING,
        RANDOMACTION,
        WAITING,
        ACTION,
        DEAD
    }
    public TurnState currentState;

    // Cooldown variables
    private float maxCD = 9f;
    private float currentCD = 0f;

    //Ienumerator variables (to see if an action has started)
    private bool hasActionStarted = false;

    // Animation variables
    private Vector3 startPosition;
    public GameObject targetToAttack;
    private float animationSpeed = 5f;

    void Start()
    {
        // Sets StateMachine to Processing
        currentState = TurnState.PROCESSING;

        // Looks for the BattleStateMachine script
        BSM = GameObject.FindObjectOfType<BattleStateMachine>();

        // Sets initial position    ***CHECK HOW THIS WORK WITH CHILD SPRITE***
        startPosition = transform.position;
    }


    void Update()
    {
        // Switch State Machine
        switch (currentState)       
        {
            case (TurnState.PROCESSING):

                // Start Filling Bar
                FillProgressBar();

                break;

            case (TurnState.RANDOMACTION):

                // Perform Random Action
                RandomAction();

                // Changes state to WAITING
                currentState = TurnState.WAITING;

                break;

            case (TurnState.WAITING):

                // Idle state

                break;

            case (TurnState.ACTION):

                // Execute IEnumerator action
                StartCoroutine(TimeForAction());

                break;

            case (TurnState.DEAD):

                break;
        }   
    }

    // ProgressBar Filling (eventually speed variable will act here)
    void FillProgressBar()    
    {
        currentCD = currentCD + Time.deltaTime;

        if (currentCD >= maxCD)
        {
            currentState = TurnState.RANDOMACTION;
        }
    }

    // Choose a random target for the attack
    void RandomAction()
    {
        HandleTurn myAttack = new HandleTurn();
        myAttack.attacker = enemy.name;
        myAttack.attackerType = "Enemy";
        myAttack.attackerGameObject = this.gameObject;
        myAttack.attackerTarget = BSM.herosInBattle[Random.Range(0, BSM.herosInBattle.Count)]; // Selects a random target from the Hero list
        BSM.StoreActions(myAttack); // Sends attack info to the BattleStateMachine StoreAction function and performList list variable
    }

    // Function to know if an action has already started
    private IEnumerator TimeForAction()
    {
        if (hasActionStarted)
        {
            yield break;
        }

        hasActionStarted = true;

        // Animate the enemy near the hero to attak
        Vector3 targetPosition = new Vector3(targetToAttack.transform.position.x +2f, targetToAttack.transform.position.y, targetToAttack.transform.position.z - 1f);
        while (MoveTowardsTarget(targetPosition))
        {
            // Do nothing while unit moves towards desired target
            yield return null;
        }


        // Wait a while
        yield return new WaitForSeconds(1f);

        // Do Damage

        // Animate back to start position
        Flip();
        Vector3 firstPosition = startPosition;
        while (MoveTowardsStart(firstPosition))
        {
            // Do nothing while unit moves towards starting position
            yield return null;
        }

        Flip();

        //  Remove this performaction from the list
        BSM.performList.RemoveAt(0);

        // Reset Battle State Machine -> WAIT
        BSM.actionState = BattleStateMachine.PerformAction.WAIT;

        // End Action
        hasActionStarted = false;

        // Reset enemy state
        currentCD = 0f;
        currentState = TurnState.PROCESSING;
    }

    //  Animation Section
    //      Move Towards the Target
    private bool MoveTowardsTarget(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animationSpeed * Time.deltaTime));
    }

    private bool MoveTowardsStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animationSpeed * (2 * Time.deltaTime)));
    }

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
