using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BaseEnemy
{
    public enum ElementalType
    {
        WATER,
        FIRE,
        FROST,
        POISON,
        ELECTRIC,
        GIANT,
        BUG
    }

    public enum Rarity
    {
        COMMON,
        UNCOMMON,
        RARE,
        SUPERRARE,
        LEGENDARY
    }

    public ElementalType enemyType;
    public Rarity enemyRarity;

    public string name;

    public float baseHP;
    public float currentHP;

    public float baseMP;
    public float currentMP;

    public float baseATK;
    public float currentATK;
    public float baseDEF;
    public float currentDEF;

    // Test
}

