using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour
{
    public BaseHero hero;

    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }

    public TurnState currentState;

    private float maxCD = 5f;
    private float currentCD = 0f;

    //Include Progress Bar Variable
    public Image ProgressBar;
   
    void Start()
    {
        currentState = TurnState.PROCESSING;
    }

 
    void Update()
    {

        // Switch State Machine
        switch (currentState)
        {
            case (TurnState.PROCESSING):

                //Start Filling Bar
                FillProgressBar();

                break;

            case (TurnState.ADDTOLIST):

                break;

            case (TurnState.WAITING):

                break;

            case (TurnState.SELECTING):

                break;

            case (TurnState.ACTION):

                break;

            case (TurnState.DEAD):

                break;
        }   
    }


    public void FillProgressBar()   // ProgressBar Filling (eventually speed variable will act here) 
    {
        currentCD = currentCD + Time.deltaTime;
        float calculateCD = (currentCD / maxCD);

        //Fill bar
        ProgressBar.transform.localScale = new Vector3((Mathf.Clamp(calculateCD, 0, 1))*3, ProgressBar.transform.localScale.y);

        if(currentCD >= maxCD)
        {
            currentState = TurnState.ADDTOLIST;
        }
    }
}
