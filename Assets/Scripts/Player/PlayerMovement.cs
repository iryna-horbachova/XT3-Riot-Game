using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;
    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;

    private float moveMultiplier = 1f;

    [SerializeField] private AudioSource jumpSoundEffect;

    void Update()
    {
        // left key is -1, right key is 1
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
        if (Input.GetButtonDown("Jump"))
        {
            jumpSoundEffect.Play();
            jump = true;
            animator.SetBool("Jumping", jump);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch")) 
        {
            crouch = false;
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime * moveMultiplier, crouch, jump);
        resetProperties();
    }

    public void OnLanding() 
    {
        animator.SetBool("Jumping", jump);
    }

    public void IncreaseMovementSpeed(float multiplier)
    {
        moveMultiplier = multiplier;
    }

    private void resetProperties() 
    {
        jump = false;
    }
}
