using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitateTowardsPlayer : MonoBehaviour
{
    public LayerMask playerMask;

    private float viewRadius = 4.0f;
    private float speed;

    void Start()
    {
        speed = Random.Range(5.0f,10.0f);
    }
    // Update is called once per frame
    void Update()
    {
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, playerMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            //float dstToTarget = Vector2.Distance(transform.position, target.position);
            transform.Translate(dirToTarget * speed * Time.deltaTime);

        }
    }
}
