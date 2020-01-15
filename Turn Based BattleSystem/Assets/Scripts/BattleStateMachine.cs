using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : MonoBehaviour
{

    //Class variables
    private EnemyStateMachine ESM;

    public enum PerformAction   // These will loop constantly until fight is over
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION
    }
    public PerformAction actionState;

    // Create List of type HandleTurn class and GameObject class  ***READ ABOUT LISTS***
    public List<HandleTurn> performList = new List<HandleTurn>();
    public List<GameObject> herosInBattle = new List<GameObject>();
    public List<GameObject> enemiesInBattle = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        actionState = PerformAction.WAIT;

        // Find all enemies in battle scene based on tagg
        enemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        herosInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
    }

    // Update is called once per frame
    void Update()
    {
        switch(actionState)
        {
            case (PerformAction.WAIT):

                // Waits until the action list has at least one action to perfom (sent from heroes or enemies)
                if(performList.Count > 0)
                {
                    actionState = PerformAction.TAKEACTION;
                }

                break;

            case (PerformAction.TAKEACTION):
                // Get GameObject which is performing the action
                GameObject actionPerformer = GameObject.Find(performList[0].attacker);

                // Choose action based on type of attacker
                if (performList[0].attackerType == "Enemy")
                {
                    ESM = actionPerformer.GetComponent<EnemyStateMachine>();
                    ESM.targetToAttack = performList[0].attackerTarget;
                    ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                }

                if (performList[0].attackerType == "Hero")
                {

                }

                // Go to next Battle State
                actionState = PerformAction.PERFORMACTION;

                break;

            case (PerformAction.PERFORMACTION):

                break;
        }
    }

    // Stores actions taken from the EnemyStateMachine and HeroStateMachine
    public void StoreActions(HandleTurn action)
    {

        // Adds action to a list
        performList.Add(action);
    }
}
