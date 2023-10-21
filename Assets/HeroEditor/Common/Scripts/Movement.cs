using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class Movement : MonoBehaviour
{
    public Character Character;
    public CharacterController Controller;

    private Vector2 _direction;
    private Vector3 _speed;

    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private int _jumpCount = 0;

    public void Start()
    {
        Character.Animator.SetBool("Ready", true);
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
                //_direction.y = 1;
                _speed.y = 10;
                _jumpCount++;
            }
        }
        if (value.canceled)
        {
            _direction.y = 0;
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
        Move(_direction);
    }

    public void Move(Vector2 direction)
    {
        if (IsGrounded())
        {
            _speed = new Vector3(5 * direction.x, _speed.y);
            
            if (direction.x != 0) 
            { 
                Turn(direction.x);
            }
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
            // Reset jump count when grounded
            _jumpCount = 0;
        }
        else
        {
            Character.SetState(CharacterState.Jump);
        }

        _speed.y -= 25 * Time.deltaTime;
        Controller.Move(_speed * Time.deltaTime);
    }

    public void Turn(float direction)
    {
        Character.transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(Controller.transform.position, groundDistance, groundMask);
    }
}
