using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    private static CameraEffects _instance;

    public static CameraEffects Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CameraEffects>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public Sprite cursorSprite;

    Vector3 oldPos;

    float shake = 0;
    float shakeAmount = 0.1f;
    float decreaseFactor = 1.0f;

    void Start()
    {
        oldPos = transform.position;
        //Cursor.SetCursor(cursorSprite.texture, Vector2.one, CursorMode.Auto);
    }

    void Update()
    {
        if (shake > 0)
        {
            oldPos = transform.position;
            transform.localPosition = new Vector3(oldPos.x + Random.insideUnitSphere.x * shakeAmount, oldPos.y + Random.insideUnitSphere.y * shakeAmount, oldPos.z);
            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shake = 0f;
        }
    } 

    public void ShakeScreen(float amountOfShake, float decreaseAmount)
    {
        oldPos = transform.position;

        shake += amountOfShake;
        decreaseFactor = decreaseAmount;
    }

    public void ShakeScreen(float amountOfShake)
    {
        oldPos = transform.position;

        shake += amountOfShake;
        decreaseFactor = 1f;
    }
}
