using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPooler : MonoBehaviour
{
    private static EffectPooler _instance;

    public static EffectPooler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<EffectPooler>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    //---------------------
    // Multiple use effects
    //---------------------
    //Blood effects
    public GameObject bloodEffectPrefab;
    public int bloodEffectPoolSize = 100;
    private List<GameObject> bloodEffectPoolList;

    //Bullet Hit Effects
    public GameObject bulletEffectPrefab;
    public int bulletEffectPoolSize = 100;
    private List<GameObject> bulletEffectPoolList;

    //--------------------
    //Singular use effects
    //--------------------
    //DashCloud Effects
    public GameObject dashCloudEffect;

    void Start()
    {
        //Blood prefabs
        bloodEffectPoolList = new List<GameObject>();
        for (int i = 0; i < bloodEffectPoolSize; i++)
        {
            GameObject b = Instantiate(bloodEffectPrefab);
            b.transform.SetParent(transform);
            b.SetActive(false);
            bloodEffectPoolList.Add(b);
        }

        //Bullet prefabs
        bulletEffectPoolList = new List<GameObject>();
        for (int i = 0; i < bulletEffectPoolSize; i++)
        {
            GameObject b = Instantiate(bulletEffectPrefab);
            b.transform.SetParent(transform);
            b.SetActive(false);
            bulletEffectPoolList.Add(b);
        }

        //Dashcloud prefab
        dashCloudEffect = Instantiate(dashCloudEffect);
        dashCloudEffect.transform.SetParent(transform);
        dashCloudEffect.SetActive(false);
    }

    public GameObject GetBulletEffect()
    {
        for (int i = 0; i < bulletEffectPoolList.Count; i++)
        {
            if (!bulletEffectPoolList[i].activeInHierarchy)
            {
                return bulletEffectPoolList[i];
            }
        }
        return null;
    }

    public GameObject[] GetBloodGroup(int bloodAmount)
    {
        GameObject[] effects = new GameObject[bloodAmount];

        for (int i = 0; i < effects.Length; i++)
        {
            for (int j = 0; j < bloodEffectPoolList.Count; j++)
            {
                if (!bloodEffectPoolList[j].activeInHierarchy)
                {
                    effects[i] = bloodEffectPoolList[j];
                }
            }
        }
        return effects;
    }

    public GameObject GetDashEffect()
    {
        return dashCloudEffect;
    }
}
