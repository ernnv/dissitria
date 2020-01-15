using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HandleTurn
{
    public string attacker;                     // Attack turn character name
    public string attackerType;                 // Attack turn character type (Hero / Enemy)
    public GameObject attackerGameObject;       // Attack turn character Game Object to handle animations and other specific variables
    public GameObject attackerTarget;           // Attack turn character target
}
