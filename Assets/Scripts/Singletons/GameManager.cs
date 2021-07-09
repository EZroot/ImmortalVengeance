using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public TextMeshProUGUI goldUI;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI dashUI;
    
    private int gold = 0;
    private int score = 0;
    private int health = 4;

    public int Gold { get { return gold; } set { gold = value; } }
    public int Score { get { return score; } set { score = value; } }
    public int Health { get { return health; } set { health = value; } }

    public void AddDamage(int value)
    {
        this.health -= value;
    }

    public void AddScore(int value)
    {
        this.score += value;
        scoreUI.text = "Score "+this.score.ToString();
    }

    public void AddGold(int value)
    {
        this.gold += value;
        goldUI.text = "Gold "+ this.gold.ToString();
    }

    public void SetDashUI(float amount)
    {
        dashUI.text = "DASH "+amount.ToString()+"%";
    }
}
