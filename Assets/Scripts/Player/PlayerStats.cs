using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int gold = 0;
    private int score = 0;
    private int health = 4;

    public int Gold {get { return gold; } set { gold = value; }}
    public int Score {get { return score; } set { score = value; }}
    public int Health {get { return health; } set { health = value; }}
    
    public void AddDamage(int value)
    {
        this.health -= value;
    }

    public void AddScore(int value)
    {
        this.score += value;
    }

    public void AddGold(int value)
    {
        this.gold += value;
    }
}
