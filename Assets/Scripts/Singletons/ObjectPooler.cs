using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private static ObjectPooler _instance;

    public static ObjectPooler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ObjectPooler>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public GameObject canvasHitpointPrefab;
    public int canvasHitpointPoolSize = 100;
    private List<GameObject> canvasHitpointList;

    public GameObject playerBulletPrefab;
    public int playerBulletPoolSize = 100;
    private List<GameObject> playerBulletList;

    public GameObject enemyBulletPrefab;
    public int enemyBulletPoolSize = 100;
    private List<GameObject> enemyBulletList;

    public GameObject goldPrefab;
    public int goldPoolSize = 500;
    private List<GameObject> goldList;

    private void Start()
    {
        //Canvas hitpoints
        canvasHitpointList = new List<GameObject>();
        for (int i = 0; i < canvasHitpointPoolSize; i++)
        {
            GameObject b = Instantiate(canvasHitpointPrefab);
            b.transform.SetParent(transform);
            b.SetActive(false);
            canvasHitpointList.Add(b);
        }

        //Player Bullet prefabs
        playerBulletList = new List<GameObject>();
        for (int i = 0; i < playerBulletPoolSize; i++)
        {
            GameObject b = Instantiate(playerBulletPrefab);
            b.transform.SetParent(transform);
            b.SetActive(false);
            playerBulletList.Add(b);
        }

        //Enemy bullet prefabs
        enemyBulletList = new List<GameObject>();
        for (int i = 0; i < enemyBulletPoolSize; i++)
        {
            GameObject b = Instantiate(enemyBulletPrefab);
            b.transform.SetParent(transform);
            b.SetActive(false);
            enemyBulletList.Add(b);
        }

        //Gold
        goldList = new List<GameObject>();
        for (int i = 0; i < goldPoolSize; i++)
        {
            GameObject b = Instantiate(goldPrefab);
            b.transform.SetParent(transform);
            b.SetActive(false);
            goldList.Add(b);
        }
    }

    public GameObject GetCanvasHitpoint()
    {
        for (int i = 0; i < canvasHitpointList.Count; i++)
        {
            if (!canvasHitpointList[i].activeInHierarchy)
                return canvasHitpointList[i];
        }
        return null;
    }

    public GameObject GetPlayerBullet()
    {
        for (int i = 0; i < playerBulletList.Count; i++)
        {
            if (!playerBulletList[i].activeInHierarchy)
                return playerBulletList[i];
        }
        return null;
    }

    public GameObject GetEnemyBullet()
    {
        for (int i = 0; i < enemyBulletList.Count; i++)
        {
            if (!enemyBulletList[i].activeInHierarchy)
                return enemyBulletList[i];
        }
        return null;
    }

    /*public GameObject GetGold()
    {
        for (int i = 0; i < enemyBulletList.Count; i++)
        {
            if (!enemyBulletList[i].activeInHierarchy)
                return enemyBulletList[i];
        }
        return null;
    }*/

    public GameObject[] GetGold(int amount)
    {
        GameObject[] gp = new GameObject[amount];

        for (int i = 0; i < gp.Length; i++)
        {
            for (int j = 0; j < goldList.Count; j++)
            {
                if (!goldList[j].activeInHierarchy)
                {
                    gp[i] = goldList[j];
                    break;
                }
            }
        }
        return gp;
    }
}
