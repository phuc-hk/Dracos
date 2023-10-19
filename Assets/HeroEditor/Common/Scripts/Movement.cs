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

    public void Start()
    {
        Character.Animator.SetBool("Ready", true);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        _direction = value.ReadValue<Vector2>();
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
            _speed = new Vector3(5 * direction.x, 10 * direction.y);
            
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
        }
        else
        {
            Character.SetState(CharacterState.Jump);
        }

        _speed.y -= 25 * Time.deltaTime; // Depends on project physics settings
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
