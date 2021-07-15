using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerAnimator animator;

    private string itemPickupLayer = "ItemPickup";
    private string goldPickupLayer = "GoldPickup";

    private Rigidbody2D rb;
    private MouseAngle mouseAngle;

    private float speed = 6;
    private Vector2 velocity;
    private float moveLimiter = 0.7f;
    private float dashAmount = 15;
    private float dashSpeed = 0f;

    private float hort, vert;

    void Start()
    {
        //Rigidbody
        rb = GetComponent<Rigidbody2D>();
        mouseAngle = GetComponent<MouseAngle>();
    }


    void Update()
    {
        //Input + Movement
        hort = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");

        //Slowdown after dash
        if (dashSpeed > 0)
        {
            dashSpeed -= Time.deltaTime * 100f;
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

        if (hort != 0 && vert != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            hort *= moveLimiter;
            vert *= moveLimiter;
        }

        velocity = new Vector2(hort, vert);

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

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * (speed + dashSpeed) * Time.fixedDeltaTime);

               //Player Animation
        animator.IsMoving = rb.velocity.magnitude > 0 ? true : false;
        animator.ParameterSpeed = rb.velocity.magnitude * 0.5f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Gold Pickup
        if (other.gameObject.layer == LayerMask.NameToLayer(goldPickupLayer))
        {
            //Remove circle trigger from gun
            other.gameObject.SetActive(false);
            GameManager.Instance.AddGold(1);
        }

        //Item pickup
        //stores it,changes stats
    }
}
