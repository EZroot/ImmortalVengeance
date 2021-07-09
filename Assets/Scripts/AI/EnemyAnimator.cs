using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer enemySpriteRenderer;

    public bool Flip
    {
        get
        {
            return enemySpriteRenderer.flipX;
        }
        set
        {
            enemySpriteRenderer.flipX = value;
        }
    }
    public float ParameterSpeed { get { return animator.GetFloat("Speed"); } set { animator.SetFloat("Speed", value); } }
    public bool IsMoving { get { return animator.GetBool("IsMoving"); } set { animator.SetBool("IsMoving", value); } }
}
