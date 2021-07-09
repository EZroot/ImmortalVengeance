using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimator : MonoBehaviour
{
    //public Animator animator;
    private SpriteRenderer gunSpriteRenderer;

    public bool Flip
    {
        get
        {
            return gunSpriteRenderer.flipY;
        }
        set
        {
            gunSpriteRenderer.flipY = value;
        }
    }
    //public float ParameterSpeed { get { return animator.GetFloat("Speed"); } set { animator.SetFloat("Speed", value); } }
    //public bool IsMoving  { get { return animator.GetBool("IsMoving"); } set { animator.SetBool("IsMoving", value); } }

    public void SetSpriteRenderer(SpriteRenderer renderer)
    {
        gunSpriteRenderer = renderer;
    }
}