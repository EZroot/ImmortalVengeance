using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private float startHeight;
    private float followRate = 4f;
    
    void Start()
    {
        startHeight = transform.position.z;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + new Vector3(0,0,startHeight), Time.deltaTime * followRate);
    }
}
