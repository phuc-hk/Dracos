using Assets.HeroEditor.Common.CharacterScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class Movement : MonoBehaviour
{
    public Character Character;
    public CharacterController Controller;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;
    private Vector2 _direction;
    private Vector3 _speed;
    private float fallSpeed = 25;
    private int _jumpCount = 0;

    [Header("Dash parameter")]  
    public float dashForce = 10f;
    public float dashCoolDown = 2f;
    //public float dashTime = 1f;
    private bool _canDash = false;
    private bool _isDashing = false;
    [SerializeField] GameObject dashEffect;

    [Header("Wall slide parameter")]
    [SerializeField] GameObject wallCheck;
    [SerializeField] GameObject groundCheck;
    public LayerMask wallMask;
    public float wallDistance = 0.2f;
    private float slidingSpeed = 25f;
    private bool isWallSlide = false;
    private float wallClimpSpeed = 4;
    private float wallJumpForce = 10;

    private AudioSource audioSource;
    public AudioClip defaultFootstepSound;
    private void Start()
    {
        Character.Animator.SetBool("Ready", true);
        audioSource = GetComponent<AudioSource>();
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        _direction.x = value.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if (_jumpCount < 1)
            {
                if (IsGrounded() && IsWall())       //Stand near wall
                {
                    _speed.y = wallClimpSpeed;
                }             
                else if (isWallSlide)               //On wall slidding
                {
                    _speed.y = wallClimpSpeed;
                    _jumpCount++;
                }
                else if (!IsGrounded() && !IsWall()) //In air
                {
                    _speed.y = wallClimpSpeed * 2;
                    _speed.x = _direction.x * wallJumpForce;
                    _jumpCount++;
                }                         
                else                                 //On ground
                {
                    _speed.y = 10;
                    _jumpCount++;
                }             
            }
        }
        if (value.canceled)
        {
            _direction.y = 0;
        }
    }

    public void OnDash(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            _canDash = true;
        }
        if (value.canceled)
        {
            _canDash = false;
        }
    }

    public void OnDeath(InputValue value)
    {
        if (value.isPressed)
        {
            Character.SetState(CharacterState.DeathB);
        }
    }

    private void Update()
    {
        WallSlide();
        Move(_direction);
        if (_canDash && !_isDashing) StartCoroutine(Dash());       
    }

    private IEnumerator Dash()
    {
        _isDashing = true;
        Vector3 direction = Controller.transform.right;
        direction.x *= Character.transform.localScale.x;
        Character.Jab();
        dashEffect.gameObject.SetActive(true);
        Controller.Move(direction * dashForce * Time.deltaTime);
        yield return new WaitForSeconds(dashCoolDown);
        _isDashing = false;
    }

    private void Move(Vector2 direction)
    {
        if (IsGrounded())
        {
            _speed = new Vector3(5 * direction.x, _speed.y);
        }      

        if (IsGrounded())
        {
            if (direction != Vector2.zero)
            {
                Character.SetState(CharacterState.Run);
            }
            else if (Character.GetState() < CharacterState.DeathB)
            {
                Character.SetState(CharacterState.Idle);
            }
            _jumpCount = 0;
        }
        else if (!IsWall())
        {
            Character.SetState(CharacterState.Jump);           
            _speed.y -= fallSpeed * Time.deltaTime;
        }
        if (direction.x != 0) Turn(direction.x); // Allow turning while in air
        Controller.Move(_speed * Time.deltaTime);

        if (!audioSource.isPlaying && direction.x != 0 && IsGrounded())
           audioSource.PlayOneShot(defaultFootstepSound);
    }

    private void Turn(float direction)
    {
        Character.transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
    }

    private void WallSlide()
    {
        if (IsWall() && !IsGrounded())
        {
            isWallSlide = true;
            _speed.y -= slidingSpeed *  Time.deltaTime / 8;
            Character.SetState(CharacterState.Climb);
        }
        else
        {
            isWallSlide = false;
        }    
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.transform.position, groundDistance, groundMask);
    }

    private bool IsWall()
    {
        return Physics2D.OverlapCircle(wallCheck.transform.position, wallDistance, wallMask);
    }
}
