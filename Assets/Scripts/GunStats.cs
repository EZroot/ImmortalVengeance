using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStats : MonoBehaviour
{
    //When you can shoot
    [SerializeField]
    private float shootTimer = 3;

    public float ShootTimer { get { return shootTimer; } set { shootTimer = value; } }

    public bool IsPickedUp {get;set;}

    void LateUpdate()
    {
        if(!IsPickedUp)
            transform.Rotate(Vector3.forward * -85 * Time.deltaTime);
    }
}
