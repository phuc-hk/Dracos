using Assets.FantasyMonsters.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    //public Transform player;
    public float moveSpeed = 2f;
    public float patrolRange = 5f;
    public float detectRange = 20f;

    private Rigidbody2D rb;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool movingRight = true;
    private Vector3 faceLeft;
    private Vector3 faceRight;
 

    private void Awake()
    {       
        rb = GetComponent<Rigidbody2D>();       
    }

    private void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(startPos.x + patrolRange, startPos.y, startPos.z);
        faceRight = transform.localScale;
        faceLeft = faceRight;
        faceLeft.x *= -1;
    }

    public void Patrol()
    {
        if (movingRight)
        {
            transform.localScale = faceRight;
            rb.velocity = transform.right * moveSpeed;
            if (transform.position.x >= endPos.x)
            {
                movingRight = false;
                //transform.localScale = faceLeft;
                Turn(faceLeft);
            }
        }
        else
        {
            transform.localScale = faceLeft;
            rb.velocity = -transform.right * moveSpeed;
            if (transform.position.x <= startPos.x)
            {
                movingRight = true;
                transform.localScale = faceRight;
                Turn(faceRight);
            }
        }
    }

    public void Chase(Transform player)
    {
        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        if (direction.x > 0)
        {
            //transform.localScale = faceRight;
            Turn(faceRight);
        }
        else if (direction.x < 0)
        {
            transform.localScale = faceLeft;
            Turn(faceLeft);
        }
    }

    
    public void FlyBack()
    {
        if (transform.localScale == faceLeft)
        {
            rb.AddForce(transform.right * 8000, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(- transform.right * 8000, ForceMode2D.Impulse);
        }    
        
    }

    public void Stop()
    {
        rb.velocity = Vector3.zero;
    }

    private void Turn(Vector3 facing)
    {
        transform.localScale = facing;
        
    }
}
