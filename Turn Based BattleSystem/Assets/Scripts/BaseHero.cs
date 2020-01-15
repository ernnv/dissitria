using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Each hero will then inherit this class
[System.Serializable]
public class BaseHero
{
    public string name;
    public int level;

    public float baseHP;
    public float currentHP;

    public float baseMP;
    public float currentMP;

    public float speed;
    public float avoidance;

    public int stamina;
    public int intellect;
    public int dexterity;
    public int agility;
}
