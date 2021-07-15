using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class EnemyAI : MonoBehaviour
{
    public enum AIState
    {
        idle,
        wander,
        chase,
        attack,
        flee
    }

    public AIState currentState;
    public NPCLineOfSight lineOfSight;
    public EnemyAnimator animator;

    private Rigidbody2D rb;

    public string playerBulletLayer = "PlayerBulletLayer";
    public float health = 10f;
    private int speed = 8;

    private IEnumerator LookForTargetRoutine;
    private bool isLookingForTarget = false;

    private Transform attackTarget = null;

    private float chaseToAttackTimer = 1f;
    private float chaseToAttackCounter = 0f;

    private float attackTimer = 1f;
    private float attackTimerCounter = 1f;
    private float attackDelayTimer = 1f;
    private float attackDelayCounter = 0f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public UnityEvent OnDamageEvent;
    public UnityEvent OnDeathEvent;

    void Start()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        LookForTargetRoutine = lineOfSight.FindTargetsWithDelay(0.2f);
        rb = transform.GetComponent<Rigidbody2D>();
        lineOfSight.StartCoroutine(lineOfSight.FindTargetsWithDelay(.2f));
    }

    void FixedUpdate()
    {
        //Animation
        animator.IsMoving = rb.velocity.magnitude > 0 ? true : false;
        animator.ParameterSpeed = rb.velocity.magnitude;
        //Movement
        switch (currentState)
        {
            case AIState.idle:
                Idle();
                break;
            case AIState.wander:
                Wander();
                break;
            case AIState.chase:
                Chase(attackTarget);
                //ChaseSpring(attackTarget);
                break;
            case AIState.attack:
                Attack(attackTarget);
                break;
            case AIState.flee:
                Flee();
                break;
        }
    }

    private void LookForTarget()
    {
        if (isLookingForTarget == false)
        {
            StartCoroutine(LookForTargetRoutine);
            isLookingForTarget = true;
        }
        if (lineOfSight.visibleTargets.Count > 0)
        {
            StopCoroutine(LookForTargetRoutine);
            attackTarget = lineOfSight.visibleTargets[0];
            currentState = AIState.chase;
            isLookingForTarget = false;
        }
    }

    private void Idle()
    {
        attackTarget = null;
        rb.velocity = Vector2.zero;
        LookForTarget();
    }

    private void Wander()
    {

    }

    private void Chase(Transform target)
    {
        if (Vector2.Distance(target.position, transform.position) > lineOfSight.viewRadius)
        {
            currentState = AIState.idle;
            return;
        }
        if (target == null)
            target = lineOfSight.visibleTargets[0];

        Vector2 dir = target.position - transform.position;
        var tmpVelocity = dir.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + tmpVelocity * speed * Time.fixedDeltaTime);
        chaseToAttackCounter += Time.deltaTime;

        if (chaseToAttackCounter > chaseToAttackTimer)
        {
            currentState = AIState.attack;
            chaseToAttackCounter = 0;
        }
    }

    //Cool effect
    private void ChaseSpring(Transform target)
    {
        if (Vector2.Distance(target.position, transform.position) > lineOfSight.viewRadius)
        {
            currentState = AIState.idle;
            return;
        }
        if (target == null)
            target = lineOfSight.visibleTargets[0];

        Vector2 dir = target.position - transform.position;
        rb.velocity = dir * speed * Time.deltaTime;

        chaseToAttackCounter += Time.deltaTime;

        if (chaseToAttackCounter > chaseToAttackTimer)
        {
            currentState = AIState.attack;
            chaseToAttackCounter = 0;
        }
    }

    private void Attack(Transform target)
    {
        if (attackTarget == null)
        {
            currentState = AIState.idle;
            return;
        }
        rb.velocity = Vector2.zero;
        attackDelayCounter += Time.deltaTime;
        //stops enemy and delay before shoot
        if (attackDelayCounter >= attackDelayTimer)
        {
            attackTimerCounter += Time.deltaTime;
            //shoot
            if (attackTimerCounter >= attackTimer)
            {
                GameObject tmp = ObjectPooler.Instance.GetEnemyBullet();
                if (tmp != null)
                {
                    Vector2 dir = target.position - transform.position;
                    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                    tmp.transform.localPosition = transform.position;
                    tmp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                    Physics2D.IgnoreCollision(tmp.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                    tmp.SetActive(true);
                }
                attackTimer = 0;
                attackDelayCounter = 0;
                currentState = AIState.chase;
            }
        }
    }

    private void Flee()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(playerBulletLayer))
        {
            other.gameObject.SetActive(false);
            Damage(Random.Range(1, 3));
        }
    }

    public void Damage(float amount)
    {
        //Damage text popup
        GameObject hitpoints = ObjectPooler.Instance.GetCanvasHitpoint();
        hitpoints.transform.localPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        hitpoints.SetActive(true);

        HitpointEffect vfx = hitpoints.GetComponent<HitpointEffect>();
        vfx.SetText("-" + amount.ToString());

        //Blood effects
        GameObject[] bloodParticles = EffectPooler.Instance.GetBloodGroup(6);
        SpawnParticlesRandom(bloodParticles,true);


            GameObject[] goldPieces = ObjectPooler.Instance.GetGold(4);
            SpawnParticlesRandom(goldPieces);
        //Damage to health
        health -= amount;

        //Shake Camera
        //CameraEffects.Instance.ShakeScreen(0.2f, 2f);

        OnDamageEvent.Invoke();
        //Death
        if (health <= 0)
        {
            OnDeathEvent.Invoke();
            Destroy(this.gameObject);
        }
    }

    public void Flash()
    {
        StartCoroutine(FlashEffect());
    }

    IEnumerator FlashEffect()
    {
        float flashTime = 0f;
        while (flashTime < 1f)
        {
            spriteRenderer.color = Color.Lerp(Color.red, originalColor, flashTime);
            transform.localPosition = new Vector2(transform.position.x, transform.position.y + Time.deltaTime);
            flashTime += Time.deltaTime * 6;
            yield return null;
        }
    }

    public void SpawnParticlesRandom(GameObject[] group, bool useScaleOffset=false)
    {
        for (int i = 0; i < group.Length; i++)
        {
            GameObject o = group[i];
            o.SetActive(true);
            float scaleOffset = 1;
            if(useScaleOffset)
                scaleOffset = Random.Range(0.25f, .5f);

            float offsetx = Random.Range(-scaleOffset, scaleOffset);
            float offsety = Random.Range(-scaleOffset, scaleOffset);

            o.transform.localPosition = new Vector2(transform.position.x + offsetx, transform.position.y + offsety);
            o.transform.localScale = new Vector2(scaleOffset, scaleOffset);
            if(useScaleOffset)
                o.transform.rotation = Quaternion.Euler(0, 0, scaleOffset * 50f);
        }
    }

}
