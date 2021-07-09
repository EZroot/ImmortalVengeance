using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerAnimator animator;

    private Rigidbody2D rb;
    private MouseAngle mouseAngle;

    private float speed = 200;
    private float moveLimiter = 0.7f;
    private float dashAmount = 250f;
    private float dashSpeed = 0f;

    private float hort, vert;

    void Start()
    {
        //Rigidbody
        rb = GetComponent<Rigidbody2D>();
        mouseAngle = GetComponent<MouseAngle>();
    }

    void FixedUpdate()
    {
        if (hort != 0 && vert != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            hort *= moveLimiter;
            vert *= moveLimiter;
        }
        rb.velocity = new Vector2(hort, vert) * (speed + dashSpeed) * Time.fixedDeltaTime;
        //Player + Gun Animation (Maybe should make this just a bool...)]
        animator.IsMoving = rb.velocity.magnitude > 0 ? true : false;
        animator.ParameterSpeed = rb.velocity.magnitude * 0.5f;
    }

    void Update()
    {
        //Input + Movement
        hort = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");

        //Slowdown after dash
        if (dashSpeed > 0)
        {
            dashSpeed -= Time.deltaTime * 500f;
            //GameManager.Instance.SetDashUI(Mathf.Clamp(dashSpeed,0,100).ToString);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (dashSpeed <= 0)
            {
                GameObject tmp = EffectPooler.Instance.GetDashEffect();
                tmp.transform.position = transform.position;
                tmp.SetActive(true);
                dashSpeed = dashAmount;
            }
        }

        //Sprite flipping
        if (mouseAngle.Angle >= -90 && mouseAngle.Angle <= 90)
        {
            animator.Flip = false;
        }
        else
        {
            animator.Flip = true;
        }
    }

    void LateUpdate()
    {
        //Player + Gun Animation (Maybe should make this just a bool...)]
        //    animator.IsMoving = rb.velocity.magnitude > 0 ? true : false;
        //    animator.ParameterSpeed = rb.velocity.magnitude * 0.5f;
    }
}
