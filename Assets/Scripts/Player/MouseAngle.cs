using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAngle : MonoBehaviour
{
    private float angle = 0;
    
    public float Angle {get {return angle;}}

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objPos.x;
        mousePos.y = mousePos.y - objPos.y;

        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
    }
}
